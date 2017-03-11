using System;
using System.Threading.Tasks;
using System.Xml.XPath;
using Parroter.Extensions;
using Parroter.Parrot.Controls.Noise;
using Parroter.Parrot.Resource;

namespace Parroter.Parrot.Controls.ConcertHall
{
    internal class ConcertHallControl
    {
        private readonly ParrotClient _parrotClient;

        public ConcertHallControl(ParrotClient parrotClient)
        {
            _parrotClient = parrotClient;
            _parrotClient.ConnectedEvent += ParrotClientOnConnectedEvent;
            _parrotClient.NotificationEvent += ParrotClientOnNotificationEvent;
        }

        // Properties
        public bool Enabled { get; private set; }

        public ConcertHallRoomType Type { get; private set; }

        public int Angle { get; private set; }

        // Modify properties asynchronously
        public async Task SetEnabledAsync(bool state)
        {
            // Don't have to change if this is already the current state.
            if (Enabled == state) return;

            await SetConcertHallEnabledAsync(state);

            Enabled = state;
            ChangedEvent?.AsyncSafeInvoke(this, EventArgs.Empty);
        }

        public async Task SetTypeAsync(ConcertHallRoomType type)
        {
            // Don't have to change if this is already the current type.
            if (Type == type) return;

            await SetConcertHallRoomAsync(type);
            
            Type = type;
            ChangedEvent?.AsyncSafeInvoke(this, EventArgs.Empty);
        }

        // Event handlers
        private async void ParrotClientOnConnectedEvent(object sender, EventArgs eventArgs)
        {
            var concertHall = await GetConcertHall();

            Enabled = concertHall.enabled;
            Type = concertHall.type;
            Angle = concertHall.angle;

            // Dispatch initial event so the UI can update.
            ChangedEvent?.AsyncSafeInvoke(this, EventArgs.Empty);
        }

        private void ParrotClientOnNotificationEvent(object sender, NotifyEventArgs notifyEventArgs)
        {
            if (notifyEventArgs.Resource != ResourceType.ConcertHallGet) return;

            throw new NotImplementedException();
        }

        // Parrot interaction
        private async Task<bool> GetConcertHallEnabledAsync()
        {
            var concertHall = await _parrotClient.SendMessageAsync(new ParrotMessage(ResourceType.ConcertHallEnabledGet));
            var concertHallValue = concertHall.XPathSelectElement("/audio/sound_effect").GetAttribute("enabled");

            return concertHallValue.Equals("true");
        }

        private async Task SetConcertHallEnabledAsync(bool state)
        {
            await _parrotClient.SendMessageAsync(new ParrotMessage(ResourceType.ConcertHallEnabledSet, state.ToString().ToLower()));
        }

        private async Task<(bool enabled, ConcertHallRoomType type, int angle)> GetConcertHall()
        {
            var concertHall = await _parrotClient.SendMessageAsync(new ParrotMessage(ResourceType.ConcertHallGet));
            var concertHallElement = concertHall.XPathSelectElement("/audio/sound_effect");

            var enabled = concertHallElement.GetAttribute("enabled");
            var roomStr = concertHallElement.GetAttribute("room_size");
            var angleStr = concertHallElement.GetAttribute("angle");

            if (!Enum.TryParse(roomStr, true, out ConcertHallRoomType roomSizeType))
                throw new Exception($"Unknown room type {roomStr}.");

            if (!int.TryParse(angleStr, out int angle))
                throw new Exception($"Invalid integer {angleStr}.");

            return (
                enabled.Equals("true"),
                roomSizeType,
                angle
            );
        }

        // get..

        private async Task SetConcertHallRoomAsync(ConcertHallRoomType type)
        {
            await _parrotClient.SendMessageAsync(new ParrotMessage(ResourceType.ConcertHallRoomSet, type.ToString().ToLower()));
        }

        // Events
        public event EventHandler<EventArgs> ChangedEvent;
    }
}
