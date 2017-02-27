using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using InTheHand.Net.Sockets;
using Parroter.Extensions;
using Parroter.Parrot.Controls;
using Parroter.Parrot.Resource;

namespace Parroter.Parrot
{
    /// <summary>
    ///     A client for communication with the Parrot RF Service.
    /// </summary>
    internal class ParrotClient
    {
        private readonly Guid[] _parrotRfServiceGuids = {
            Guid.Parse("8b6814d3-6ce7-4498-9700-9312c1711f64")
        };

        private readonly Semaphore _waitingList;

        public ParrotClient(BluetoothDeviceInfo device)
        {
            Device = device;
            BluetoothClient = new BluetoothClient();
            NoiseControl = new NoiseControl(this);

            _waitingList = new Semaphore(1, 1);
        }
        
        public BluetoothDeviceInfo Device { get; }

        private BluetoothClient BluetoothClient { get; }

        public NoiseControl NoiseControl { get; }

        public bool Connected => BluetoothClient.Connected;

        /// <summary>
        ///     Connects to the Parrot RF Service, if that succeeds
        ///     it starts waiting for data to handle. 
        /// 
        ///     Throws an exception when the connection failed.
        /// </summary>
        public async Task ConnectAsync()
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

            await BluetoothClient.GetStream().WriteAsync(initialPacket, 0, initialPacket.Length);
            await BluetoothClient.GetStream().FlushAsync();
            await BluetoothClient.GetStream().ReadAsync(new byte[3], 0, 3);

            // Dispatch event
            ConnectedEvent.AsyncSafeInvoke(this, EventArgs.Empty);
        }

        public async Task<int> GetBatteryPercentAsync()
        {
            var battery = await SendMessageAsync(new ParrotMessage(ResourceType.BatteryGet));
            var batteryValue = battery.XPathSelectElement("/system/battery").Attribute("percent")?.Value;
            if (batteryValue == null)
                throw new Exception(nameof(batteryValue));

            return int.Parse(batteryValue);
        }

        /// <summary>
        ///     Sends a message to the Parrot RF Service.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<XElement> SendMessageAsync(ParrotMessage message)
        {
            if (!Connected)
                throw new Exception($"{nameof(ParrotClient)} is not connected.");

            _waitingList.WaitOne();

            try
            {
                // Debug message
                Console.WriteLine($"Sending message '{message.Request}'");

                // Send message to Parrot
                var messageBytes = message.GetRequest();

                await BluetoothClient.GetStream().WriteAsync(messageBytes, 0, messageBytes.Length);
                await BluetoothClient.GetStream().FlushAsync();

                // Receive the response
                var responseBuffer = new byte[1024];
                await BluetoothClient.GetStream().ReadAsync(responseBuffer, 0, responseBuffer.Length);

                // Parse response
                var responseLength = (short)((responseBuffer[0] << 8) | (responseBuffer[1] << 0));
                var element = XElement.Parse(Encoding.ASCII.GetString(responseBuffer, 7, responseLength - 7));

                // Notifications
                var notifyElement = element.XPathSelectElement("/notify");
                var notifyPath = notifyElement?.Attribute("path")?.Value;
                if (notifyPath != null)
                {
                    var resource = ResourceManager.Resources.FirstOrDefault(x => x.Value.Equals(notifyPath)).Key;

                    NotificationEvent?.AsyncSafeInvoke(this, new NotifyEventArgs(resource, notifyPath));
                }

                // Return
                return element;
            }
            finally
            {
                _waitingList.Release();
            }
        }

        public event EventHandler<EventArgs> ConnectedEvent;

        public event EventHandler<NotifyEventArgs> NotificationEvent;
    }
}
