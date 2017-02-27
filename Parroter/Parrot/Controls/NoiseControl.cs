using System;
using System.Threading.Tasks;
using System.Xml.XPath;
using Parroter.Extensions;
using Parroter.Parrot.Resource;

namespace Parroter.Parrot.Controls
{
    internal class NoiseControl
    {
        private readonly ParrotClient _parrotClient;

        public NoiseControl(ParrotClient parrotClient)
        {
            _parrotClient = parrotClient;
            _parrotClient.ConnectedEvent += ParrotClientOnConnectedEvent;
            _parrotClient.NotificationEvent += ParrotClientOnNotificationEvent;
        }

        public bool Enabled { get; private set; }

        // Modify properties
        public async Task SetEnabledAsync(bool state)
        {
            // Don't have to change if this is already the current state.
            if (Enabled == state) return;

            await SetNoiseControlEnabledAsync(state);

            Enabled = state;
            ChangedEvent?.AsyncSafeInvoke(this, EventArgs.Empty);
        }

        // Event handlers
        private async void ParrotClientOnConnectedEvent(object sender, EventArgs eventArgs)
        {
            Enabled = await GetNoiseControlEnabledAsync();

            // Dispatch initial event so the UI can update.
            ChangedEvent?.AsyncSafeInvoke(this, EventArgs.Empty);
        }

        private async void ParrotClientOnNotificationEvent(object sender, NotifyEventArgs notifyEventArgs)
        {
            if (notifyEventArgs.Resource == ResourceType.NoiseControlGet)
            {
                await GetNoiseControlAsync();
            }
        }

        // Parrot interaction
        private async Task<bool> GetNoiseControlEnabledAsync()
        {
            var noiseControl = await _parrotClient.SendMessageAsync(new ParrotMessage(ResourceType.NoiseControlEnabledGet));
            var noiseControlValue = noiseControl.XPathSelectElement("/audio/noise_control").Attribute("enabled")?.Value;
            if (noiseControlValue == null)
                throw new NullReferenceException(nameof(noiseControlValue));

            return noiseControlValue.Equals("true");
        }

        private async Task SetNoiseControlEnabledAsync(bool state)
        {
            await _parrotClient.SendMessageAsync(new ParrotMessage(ResourceType.NoiseControlEnabledSet, state.ToString().ToLower()));
        }

        private async Task GetNoiseControlAsync()
        {
            var noiseControl = await _parrotClient.SendMessageAsync(new ParrotMessage(ResourceType.NoiseControlGet));

            Console.WriteLine(noiseControl);
        }

        // Events
        public event EventHandler<EventArgs> ChangedEvent;
    }
}
