namespace Parroter.Parrot
{
    /// <summary>
    ///     Reverse engineered from the Parrot Zik android app.
    ///     https://play.google.com/store/apps/details?id=com.parrot.zik2
    /// </summary>
    internal static class ZikApi
    {
        public const string AccountUsernameGet = "/api/account/username/get";

        public const string AccountUsernameSet = "/api/account/username/set";

        public const string AppliVersionSet = "/api/appli_version/set";

        public const string AudioAncPhoneModeGet = "/api/audio/noise_control/phone_mode/get";

        public const string AudioAncPhoneModeSet = "/api/audio/noise_control/phone_mode/set";

        public const string AudioNoiseControlAutoNcSet = "/api/audio/noise_control/auto_nc/set";

        public const string AudioNoiseGet = "/api/audio/noise/get";

        public const string AudioParamEqValueSet = "/api/audio/param_equalizer/value/set";

        public const string AudioPresetActivate = "/api/audio/preset/activate";

        public const string AudioPresetBypassGet = "/api/audio/preset/bypass/get";

        public const string AudioPresetBypassSet = "/api/audio/preset/bypass/set";

        public const string AudioPresetClearAll = "/api/audio/preset/clear_all";

        public const string AudioPresetCounterGet = "/api/audio/preset/counter/get";

        public const string AudioPresetCurrentGet = "/api/audio/preset/current/get";

        public const string AudioPresetDownload = "/api/audio/preset/download";

        public const string AudioPresetProducerCancel = "/api/audio/preset/cancel_producer";

        public const string AudioPresetRemove = "/api/audio/preset/remove";

        public const string AudioPresetSave = "/api/audio/preset/save";

        public const string AudioPresetSynchroStart = "/api/audio/preset/synchro/start";

        public const string AudioPresetSynchroStop = "/api/audio/preset/synchro/stop";

        public const string AudioSmartTuneGet = "/api/audio/smart_audio_tune/get";

        public const string AudioSmartTuneSet = "/api/audio/smart_audio_tune/set";

        public const string AudioSourceGet = "/api/audio/source/get";

        public const string AudioTrackMetadataGet = "/api/audio/track/metadata/get";

        //        public const string BatteryGet = "/api/system/battery/get";

        //        public const string ConcertHallAngleGet = "/api/audio/sound_effect/angle/get";

        //        public const string ConcertHallAngleSet = "/api/audio/sound_effect/angle/set";

        //        public const string ConcertHallEnabledGet = "/api/audio/sound_effect/enabled/get";

        //        public const string ConcertHallEnabledSet = "/api/audio/sound_effect/enabled/set";

        //        public const string ConcertHallGet = "/api/audio/sound_effect/get";

        public const string ConcertHallModeGet = "/api/audio/sound_effect/mode/get";

        //        public const string ConcertHallRoomGet = "/api/audio/sound_effect/room_size/get";
        //
        //        public const string ConcertHallRoomSet = "/api/audio/sound_effect/room_size/set";

        public const string EqualizerEnabledGet = "/api/audio/equalizer/enabled/get";

        public const string EqualizerEnabledSet = "/api/audio/equalizer/enabled/set";

        public const int FeatureAutonc = 2048;

        public const int FeatureBluetoothDelay = 65536;

        public const int FeatureNcDuringCall = 4096;

        public const string FriendlyNameGet = "/api/bluetooth/friendlyname/get";

        public const string FriendlyNameSet = "/api/bluetooth/friendlyname/set";

        //        public const string NoiseControlEnabledGet = "/api/audio/noise_control/enabled/get";

        //        public const string NoiseControlEnabledSet = "/api/audio/noise_control/enabled/set";

        //        public const string NoiseControlGet = "/api/audio/noise_control/get";

        //        public const string NoiseControlSet = "/api/audio/noise_control/set";

        public const string SoftwareDownloadSizeSet = "/api/software/download_size/set";

        public const string SoftwareTtsDisable = "/api/software/tts/disable";

        public const string SoftwareTtsEnable = "/api/software/tts/enable";

        public const string SoftwareTtsGet = "/api/software/tts/get";

        public const string SoftwareVersionSip6Get = "/api/software/version/get";

        public const string SystemAudioDelayGet = "/api/audio/delay/get";

        public const string SystemAudioDelaySet = "/api/audio/delay/set";

        public const string SystemAutoConnectionGet = "/api/system/auto_connection/enabled/get";

        public const string SystemAutoConnectionSet = "/api/system/auto_connection/enabled/set";

        public const string SystemAutoPowerOffGet = "/api/system/auto_power_off/get";

        public const string SystemAutoPowerOffSet = "/api/system/auto_power_off/set";

        public const string SystemBtAddressGet = "/api/system/bt_address/get";

        public const string SystemColorGet = "/api/system/color/get";

        public const string SystemDevicePi = "/api/system/pi/get";

        public const string SystemDeviceTypeGet = "/api/system/device_type/get";

        public const string SystemFeaturesGet = "/api/features/get";

        public const string SystemFlightModeDisable = "/api/flight_mode/disable";

        public const string SystemFlightModeEnable = "/api/flight_mode/enable";

        public const string SystemFlightModeGet = "/api/flight_mode/get";

        public const string SystemHeadDetectionEnabledGet = "/api/system/head_detection/enabled/get";

        public const string SystemHeadDetectionEnabledSet = "/api/system/head_detection/enabled/set";

        public const string SystemTextureGet = "/api/system/texture/get";

        public const string ThumbEqualizerValueGet = "/api/audio/thumb_equalizer/value/get";

        public const string ThumbEqualizerValueSet = "/api/audio/thumb_equalizer/value/set";

        public static readonly string[] KnowMessages =
        {
            "/api/account/username/get",
            "/api/account/username/set",
            "/api/appli_version/set",
            "/api/audio/noise_control/phone_mode/get",
            "/api/audio/noise_control/phone_mode/set",
            "/api/audio/noise_control/auto_nc/set",
            "/api/audio/noise/get",
            "/api/audio/param_equalizer/value/set",
            "/api/audio/preset/activate",
            "/api/audio/preset/bypass/get",
            "/api/audio/preset/bypass/set",
            "/api/audio/preset/clear_all",
            "/api/audio/preset/counter/get",
            "/api/audio/preset/current/get",
            "/api/audio/preset/download",
            "/api/audio/preset/cancel_producer",
            "/api/audio/preset/remove",
            "/api/audio/preset/save",
            "/api/audio/preset/synchro/start",
            "/api/audio/preset/synchro/stop",
            "/api/audio/smart_audio_tune/get",
            "/api/audio/smart_audio_tune/set",
            "/api/audio/source/get",
            "/api/audio/track/metadata/get",
            "/api/audio/sound_effect/mode/get",
            "/api/audio/equalizer/enabled/get",
            "/api/audio/equalizer/enabled/set",
            "/api/bluetooth/friendlyname/get",
            "/api/bluetooth/friendlyname/set",
            "/api/software/download_size/set",
            "/api/software/tts/disable",
            "/api/software/tts/enable",
            "/api/software/tts/get",
            "/api/software/version/get",
            "/api/audio/delay/get",
            "/api/audio/delay/set",
            "/api/system/auto_connection/enabled/get",
            "/api/system/auto_connection/enabled/set",
            "/api/system/auto_power_off/get",
            "/api/system/auto_power_off/set",
            "/api/system/bt_address/get",
            "/api/system/color/get",
            "/api/system/pi/get",
            "/api/system/device_type/get",
            "/api/features/get",
            "/api/flight_mode/disable",
            "/api/flight_mode/enable",
            "/api/flight_mode/get",
            "/api/system/head_detection/enabled/get",
            "/api/system/head_detection/enabled/set",
            "/api/system/texture/get",
            "/api/audio/thumb_equalizer/value/get",
            "/api/audio/thumb_equalizer/value/set",
        };
    }
}
