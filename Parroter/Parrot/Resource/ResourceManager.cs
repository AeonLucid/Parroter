using System.Collections.Generic;

namespace Parroter.Parrot.Resource
{
    internal static class ResourceManager
    {
        static ResourceManager()
        {
            Resources = new Dictionary<ResourceType, string>
            {
                {ResourceType.BatteryGet, "/api/system/battery/get"},
                {ResourceType.NoiseControlEnabledGet, "/api/audio/noise_control/enabled/get"},
                {ResourceType.NoiseControlEnabledSet, "/api/audio/noise_control/enabled/set"},
                {ResourceType.NoiseControlGet, "/api/audio/noise_control/get"},
                {ResourceType.NoiseControlSet, "/api/audio/noise_control/set"},
                {ResourceType.ConcertHallEnabledGet, "/api/audio/sound_effect/enabled/get"},
                {ResourceType.ConcertHallEnabledSet, "/api/audio/sound_effect/enabled/set"},
                {ResourceType.ConcertHallGet, "/api/audio/sound_effect/get"},
                {ResourceType.ConcertHallRoomGet, "/api/audio/sound_effect/room_size/get"},
                {ResourceType.ConcertHallRoomSet, "/api/audio/sound_effect/room_size/set"}
            };
        }

        public static Dictionary<ResourceType, string> Resources { get; }
    }
}
