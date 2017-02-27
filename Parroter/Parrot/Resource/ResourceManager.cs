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
                {ResourceType.NoiseControlGet, "/api/audio/noise_control/get"}
            };
        }

        public static Dictionary<ResourceType, string> Resources { get; }
    }
}
