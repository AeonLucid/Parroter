using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.XPath;
using Parroter.Extensions;
using Parroter.Parrot.Resource;

namespace Parroter.Parrot.Controls.ConcertHall
{
    internal class ConcertHallControl
    {
        private static readonly int[] ValidAngles = {30, 60, 90, 120, 150, 180};

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

        public async Task SetAngleAsync(int angle)
        {
            // Don't have to change if this is already the current angle.
            if (Angle == angle) return;

            if (!ValidAngles.Contains(angle))
                throw new Exception("Invalid angle specified.");

            await SetConcertHallAngleAsync(angle);

            Angle = angle;
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

        private async Task<ConcertHallRoomType> GetConcertHallRoomAsync()
        {
            var concertHall = await _parrotClient.SendMessageAsync(new ParrotMessage(ResourceType.ConcertHallRoomGet));
            var concertHallRoomStr = concertHall.XPathSelectElement("/audio/sound_effect").GetAttribute("room_size");

            if (!Enum.TryParse(concertHallRoomStr, true, out ConcertHallRoomType roomSizeType))
                throw new Exception($"Unknown room type {concertHallRoomStr}.");

            return roomSizeType;
        }

        private async Task SetConcertHallRoomAsync(ConcertHallRoomType type)
        {
            await _parrotClient.SendMessageAsync(new ParrotMessage(ResourceType.ConcertHallRoomSet, type.ToString().ToLower()));
        }

        private async Task<int> GetConcertHallAngleAsync()
        {
            var concertHall = await _parrotClient.SendMessageAsync(new ParrotMessage(ResourceType.ConcertHallAngleGet));
            var concertHallAngleStr = concertHall.XPathSelectElement("/audio/sound_effect").GetAttribute("angle");
            
            if (!int.TryParse(concertHallAngleStr, out int angle))
                throw new Exception($"Invalid integer {concertHallAngleStr}.");

            return angle;
        }

        private async Task SetConcertHallAngleAsync(int angle)
        {
            await _parrotClient.SendMessageAsync(new ParrotMessage(ResourceType.ConcertHallAngleSet, angle.ToString()));
        }

        // Events
        public event EventHandler<EventArgs> ChangedEvent;
    }
}
