using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Linq;
using InTheHand.Net.Sockets;

namespace Parroter.Parrot
{
    /// <summary>
    ///     A client for communication with the Parrot RF Service.
    /// </summary>
    public class ParrotClient
    {
        private readonly Guid[] _parrotRfServiceGuids = {
            Guid.Parse("8b6814d3-6ce7-4498-9700-9312c1711f64")
        };

        private readonly Semaphore _waitingList;

        public ParrotClient(BluetoothDeviceInfo device)
        {
            Device = device;
            BluetoothClient = new BluetoothClient();

            _waitingList = new Semaphore(1, 1);
        }

        public BluetoothDeviceInfo Device { get; }

        private BluetoothClient BluetoothClient { get; }

        public bool Connected => BluetoothClient.Connected;

        /// <summary>
        ///     Connects to the Parrot RF Service, if that succeeds
        ///     it starts waiting for data to handle. 
        /// 
        ///     Throws an exception when the connection failed.
        /// </summary>
        public void Connect()
        {
            // Find the Parrot RF Service.
            var parrotService = Device.InstalledServices.FirstOrDefault(x => _parrotRfServiceGuids.Contains(x));
            if (parrotService == null)
                throw new Exception("Couldn't find the parrot rf service.");

            // Connect to the service.
            BluetoothClient.Connect(Device.DeviceAddress, parrotService);

            if (!Connected)
                throw new Exception("Couldn't connect to the parrot rf service.");

            // Send initial packet and stop other packets from being sent.
            var initialPacket = new byte[] { 0x00, 0x03, 0x00 };
            BluetoothClient.GetStream().Write(initialPacket, 0, initialPacket.Length);
            BluetoothClient.GetStream().Flush();
            BluetoothClient.GetStream().Read(new byte[3], 0, 3);

            // Dispatch event
            ConnectedEvent?.Invoke(this, EventArgs.Empty);
        }

        public XElement SendMessage(ParrotMessage message)
        {
            if (!Connected)
                throw new Exception($@"{nameof(ParrotClient)} is not connected.");

            _waitingList.WaitOne();

            try
            {
                // Debug message
                Console.WriteLine($@"Sending message '{message.Request}'");

                // Send message to Parrot
                var messageBytes = message.GetRequest();

                BluetoothClient.GetStream().Write(messageBytes, 0, messageBytes.Length);
                BluetoothClient.GetStream().Flush();

                // Receive the response
                var responseBuffer = new byte[1024];
                BluetoothClient.GetStream().Read(responseBuffer, 0, responseBuffer.Length);

                // Parse response
                var responseLength = (short)((responseBuffer[0] << 8) | (responseBuffer[1] << 0));

                return XElement.Parse(Encoding.ASCII.GetString(responseBuffer, 7, responseLength - 7));
            }
            finally
            {
                _waitingList.Release();
            }
        }

        public event EventHandler<EventArgs> ConnectedEvent;
    }
}
