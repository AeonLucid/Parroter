using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using InTheHand.Net.Sockets;
using Parroter.Extensions;
using Parroter.Parrot.Controls.Battery;
using Parroter.Parrot.Controls.Noise;
using Parroter.Parrot.Resource;

namespace Parroter.Parrot
{
    /// <summary>
    ///     A client for communication with the Parrot RF Service.
    /// </summary>
    internal class ParrotClient
    {
        private static readonly Guid[] ParrotRfServiceGuids = {
            Guid.Parse("8b6814d3-6ce7-4498-9700-9312c1711f64")
        };

        private readonly Semaphore _waitingList;

        public ParrotClient(BluetoothDeviceInfo device)
        {
            Device = device;
            BluetoothClient = new BluetoothClient();
            Battery = new Battery(this);
            NoiseControl = new NoiseControl(this);

            _waitingList = new Semaphore(1, 1);
        }
        
        public BluetoothDeviceInfo Device { get; }

        private BluetoothClient BluetoothClient { get; }

        // Properties used to retrieve / modify settings
        public Battery Battery { get; }

        public NoiseControl NoiseControl { get; }

        /// <summary>
        ///     Connects to the Parrot RF Service, if that succeeds
        ///     it starts waiting for data to handle. 
        /// 
        ///     Throws an exception when the connection failed.
        /// </summary>
        public async Task ConnectAsync()
        {
            // Find the Parrot RF Service.
            var parrotService = Device.InstalledServices.FirstOrDefault(x => ParrotRfServiceGuids.Contains(x));
            if (parrotService == null)
                throw new Exception("Couldn't find the parrot rf service.");

            // Connect to the service.
            BluetoothClient.Connect(Device.DeviceAddress, parrotService);

            if (!BluetoothClient.Connected)
                throw new Exception("Couldn't connect to the parrot rf service.");

            // Send initial packet and stop other packets from being sent.
            var initialPacket = new byte[] { 0x00, 0x03, 0x00 };

            await BluetoothClient.GetStream().WriteAsync(initialPacket, 0, initialPacket.Length);
            await BluetoothClient.GetStream().FlushAsync();
            await BluetoothClient.GetStream().ReadAsync(new byte[3], 0, 3);

            // Dispatch event
            ConnectedEvent.AsyncSafeInvoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Sends a message to the Parrot RF Service.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task<XElement> SendMessageAsync(ParrotMessage message)
        {
            if (!BluetoothClient.Connected)
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

                // Hold response element
                XElement answerElement;

                // Hold notifications
                var notifyElements = new List<XElement>();

                // Receive response(s)
                do
                {
                    answerElement = XElement.Parse(await ReceiveResponseAsync());

                    // Find notificiations
                    if (answerElement.Name == "notify")
                    {
                        notifyElements.Add(answerElement);
                    }
                    else if (answerElement.Name == "answer")
                    {
                        var notifyElement = answerElement.XPathSelectElement("/notify");
                        if (notifyElement != null)
                        {
                            notifyElements.Add(notifyElement);
                        }
                    }
                } while (answerElement.Name != "answer");

                // Handle notifications
                foreach (var notifyElement in notifyElements)
                {
                    var notifyPath = notifyElement?.Attribute("path")?.Value;
                    if (notifyPath == null) continue;

                    var resource = ResourceManager.Resources.FirstOrDefault(x => x.Value.Equals(notifyPath)).Key;

                    // Debug message
                    Console.WriteLine($"Dispatching notification {resource} ({notifyPath})");

                    NotificationEvent?.AsyncSafeInvoke(this, new NotifyEventArgs(resource, notifyPath));
                }

                // Return
                return answerElement;
            }
            finally
            {
                _waitingList.Release();
            }
        }

        private async Task<string> ReceiveResponseAsync()
        {
            // Get packet length bytes
            var lengthBytes = new byte[2];
            await BluetoothClient.GetStream().ReadAsync(lengthBytes, 0, 2);

            // Get packet length as short
            var responseLength = (short)((lengthBytes[0] << 8) | (lengthBytes[1] << 0));

            // Get packet byte array
            var responseBytes = new byte[responseLength];
            await BluetoothClient.GetStream().ReadAsync(responseBytes, 0, responseLength);

            // Return, cut off non-string data
            return Encoding.ASCII.GetString(responseBytes, 5, responseLength - 7);
        }

        public event EventHandler<EventArgs> ConnectedEvent;

        public event EventHandler<NotifyEventArgs> NotificationEvent;
    }
}
