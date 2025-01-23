using GTA;
using System;

namespace PoliceAI_SHV
{
    public static class Load
    {
        // Flags to enable/disable plugins
        public static bool enableRoadblocks { get; private set; }
        public static bool enableHelicopters { get; private set; }
        public static bool enableSWAT { get; private set; }
        public static bool enableSpikeStrips { get; private set; }
        public static bool enableSnipers { get; private set; }
        public static bool enableCheckpoints { get; private set; }

        // Plugin-specific settings
        public static int responseDistance { get; private set; }
        public static int roadblockTimeout { get; private set; }
        public static int helicopterSpawnDistance { get; private set; }
        public static int helicopterResponseTime { get; private set; }
        public static int swatResponseTime { get; private set; }
        public static int spikeStripResponseDistance { get; private set; }
        public static int sniperSpawnDistance { get; private set; }
        public static int checkpointSpawnDistance { get; private set; }

        // load the INI settings
        public static void LoadConfig()
        {
            try
            {
                ScriptSettings config = ScriptSettings.Load("scripts\\PoliceAISettingsSHV.ini");

                // Load plugin enable/disable flags
                enableRoadblocks = config.GetValue("General", "EnableRoadblocks", true);
                enableHelicopters = config.GetValue("General", "EnableHelicopters", true);
                enableSWAT = config.GetValue("General", "EnableSWAT", true);
                enableSpikeStrips = config.GetValue("General", "EnableSpikeStrips", true);
                enableSnipers = config.GetValue("General", "EnableSnipers", true);
                enableCheckpoints = config.GetValue("General", "EnableCheckpoints", true);

                // Load roadblocks settings
                responseDistance = config.GetValue("Roadblocks", "ResponseDistance", 500);
                roadblockTimeout = config.GetValue("Roadblocks", "RoadblockTimeout", 30000);

                // Load helicopter settings
                helicopterSpawnDistance = config.GetValue("Helicopters", "HelicopterSpawnDistance", 500);
                helicopterResponseTime = config.GetValue("Helicopters", "HelicopterResponseTime", 20000);

                // Load SWAT settings
                swatResponseTime = config.GetValue("SWAT", "SWATResponseTime", 40000);

                // Load spike strip settings
                spikeStripResponseDistance = config.GetValue("SpikeStrips", "SpikeStripResponseDistance", 200);

                // Load sniper settings
                sniperSpawnDistance = config.GetValue("Snipers", "SniperSpawnDistance", 50);

                // Load checkpoint settings
                checkpointSpawnDistance = config.GetValue("Checkpoints", "CheckpointSpawnDistance", 150);

                // Notify settings have been loaded
                GTA.UI.Notification.PostTicker("PoliceAI settings loaded", false, true);
            }
            catch (Exception ex)
            {
                // Log the error and show a notification using PostTicker
                GTA.UI.Notification.PostTicker("Failed to load PoliceAI settings: " + ex.Message, false, true);
            }
        }
    }
}
