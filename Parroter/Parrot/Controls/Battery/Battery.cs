using System;
using System.Threading.Tasks;
using System.Xml.XPath;
using Parroter.Extensions;
using Parroter.Parrot.Resource;

namespace Parroter.Parrot.Controls.Battery
{
    internal class Battery
    {
        private readonly ParrotClient _parrotClient;

        public Battery(ParrotClient parrotClient)
        {
            _parrotClient = parrotClient;
            _parrotClient.ConnectedEvent += ParrotClientOnConnectedEvent;
            _parrotClient.NotificationEvent += ParrotClientOnNotificationEvent;
        }

        // Properties
        public bool Charging { get; private set; }

        public int Percent { get; private set; }

        // Modify properties asynchronously
        public async Task RefreshAsync()
        {
            var batteryData = await GetBatteryAsync();
            if (batteryData.charging != Charging ||
                batteryData.batteryPercent != Percent)
            {
                Charging = batteryData.charging;
                Percent = batteryData.batteryPercent;

                ChangedEvent?.AsyncSafeInvoke(this, EventArgs.Empty);
            }
        }

        // Event handlers
        private async void ParrotClientOnConnectedEvent(object sender, EventArgs eventArgs)
        {
            await RefreshAsync();
        }

        private async void ParrotClientOnNotificationEvent(object sender, NotifyEventArgs notifyEventArgs)
        {
            if (notifyEventArgs.Resource != ResourceType.BatteryGet) return;

            await RefreshAsync();
        }

        // Parrot interaction
        private async Task<(bool charging, int batteryPercent)> GetBatteryAsync()
        {
            var battery = await _parrotClient.SendMessageAsync(new ParrotMessage(ResourceType.BatteryGet));
            var batteryElement = battery.XPathSelectElement("/system/battery");

            var batteryCharging = batteryElement.GetAttribute("state");
            var batteryPercent = batteryElement.GetAttribute("percent");
            var charging = batteryCharging.Equals("charging") || batteryCharging.Equals("charged");

            return (charging, int.Parse(batteryPercent));
        }

        // Events
        public event EventHandler<EventArgs> ChangedEvent;
    }
}
