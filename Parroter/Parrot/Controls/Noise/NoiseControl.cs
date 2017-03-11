using System;
using System.Threading.Tasks;
using System.Xml.XPath;
using Parroter.Extensions;
using Parroter.Parrot.Resource;

namespace Parroter.Parrot.Controls.Noise
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

        // Properties
        public bool Enabled { get; private set; }

        public NoiseControlType Type { get; private set; }

        // Modify properties asynchronously
        public async Task SetEnabledAsync(bool state)
        {
            // Don't have to change if this is already the current state.
            if (Enabled == state) return;

            await SetNoiseControlEnabledAsync(state);

            Enabled = state;
            ChangedEvent?.AsyncSafeInvoke(this, EventArgs.Empty);
        }

        public async Task SetTypeAsync(NoiseControlType type)
        {
            // Don't have to change if this is already the current type.
            if (Type == type) return;

            await SetNoiseControlAsync(type);

            Enabled = type != NoiseControlType.NoiseControlOff;
            Type = type;
            ChangedEvent?.AsyncSafeInvoke(this, EventArgs.Empty);
        }

        // Event handlers
        private async void ParrotClientOnConnectedEvent(object sender, EventArgs eventArgs)
        {
            Enabled = await GetNoiseControlEnabledAsync();
            Type = Enabled ? await GetNoiseControlAsync() : NoiseControlType.NoiseControlOff;

            // Dispatch initial event so the UI can update.
            ChangedEvent?.AsyncSafeInvoke(this, EventArgs.Empty);
        }

        private async void ParrotClientOnNotificationEvent(object sender, NotifyEventArgs notifyEventArgs)
        {
            if (notifyEventArgs.Resource != ResourceType.NoiseControlGet) return;

            var newType = await GetNoiseControlAsync();
            if (newType == Type) return;

            Type = newType;

            // Dispatch initial event so the UI can update.
            ChangedEvent?.AsyncSafeInvoke(this, EventArgs.Empty);
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

        // TODO: "auto_nc" for auto noise control.
        private async Task<NoiseControlType> GetNoiseControlAsync()
        {
            var noiseControl = await _parrotClient.SendMessageAsync(new ParrotMessage(ResourceType.NoiseControlGet));
            var noiseControlElement = noiseControl.XPathSelectElement("/audio/noise_control");

            var type = noiseControlElement.Attribute("type")?.Value;
            if (type == null)
                throw new NullReferenceException(nameof(type));

            var value = noiseControlElement.Attribute("value")?.Value;
            if (value == null)
                throw new NullReferenceException(nameof(value));

            // Determine current type.
            NoiseControlType noiseControlType;

            switch (type)
            {
                case "anc":
                    noiseControlType = value == "1" ? 
                        NoiseControlType.NoiseControlOn : 
                        NoiseControlType.NoiseControlMax;
                    break;
                case "aoc":
                    noiseControlType = value == "1" ? 
                        NoiseControlType.StreetMode : 
                        NoiseControlType.StreetModeMax;
                    break;
                case "off":
                    noiseControlType = NoiseControlType.NoiseControlOff;
                    break;
                default:
                    throw new Exception($"Unknown noise control profile (value={value} type={type}).");
            }

            return noiseControlType;
        }

        private async Task SetNoiseControlAsync(NoiseControlType type)
        {
            string value;

            switch (type)
            {
                case NoiseControlType.NoiseControlMax:
                    value = "anc&value=2";
                    break;
                case NoiseControlType.NoiseControlOn:
                    value = "anc&value=1";
                    break;
                case NoiseControlType.NoiseControlOff:
                    value = "off&value=1";
                    break;
                case NoiseControlType.StreetMode:
                    value = "aoc&value=1";
                    break;
                case NoiseControlType.StreetModeMax:
                    value = "aoc&value=2";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            await _parrotClient.SendMessageAsync(new ParrotMessage(ResourceType.NoiseControlSet, value));
        }

        // Events
        public event EventHandler<EventArgs> ChangedEvent;
    }
}
