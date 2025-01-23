using IniParser;
using IniParser.Model;
using Rage;
using System;

namespace PoliceAI_RPH_Edition
{
    public static class ConfigManager
    {
        private static FileIniDataParser _parser = new FileIniDataParser();
        private static IniData _data;

        public static bool EnableTrafficStops { get; private set; }
        public static int MaxPursuitSpeed { get; private set; }
        public static bool AllowBackup { get; private set; }
        public static float TrafficStopDistance { get; private set; }
        public static int PursuitTimeout { get; private set; }
        public static int BackupUnits { get; private set; }

        public static bool EnableRoadblocks { get; private set; }
        public static float RoadblockFrequency { get; private set; }
        public static string RoadblockDifficulty { get; private set; }

        public static bool EnableHelicopterSupport { get; private set; }
        public static int HelicopterEngagementLevel { get; private set; }

        public static bool EnableSpikeStrips { get; private set; }
        public static int SpikeStripDeploymentLevel { get; private set; }

        public static bool EnableNotifications { get; private set; }
        public static bool NotifyOnTrafficStop { get; private set; }
        public static bool NotifyOnPursuit { get; private set; }
        public static bool NotifyOnBackup { get; private set; }

        public static bool EnableLogging { get; private set; }
        public static bool CheckForUpdates { get; private set; }

        public static void LoadSettings()
        {
            try
            {
                _data = _parser.ReadFile(@"PoliceAISettingsRPH.ini");

                EnableTrafficStops = bool.Parse(_data["AI_Settings"]["EnableTrafficStops"]);
                MaxPursuitSpeed = int.Parse(_data["AI_Settings"]["MaxPursuitSpeed"]);
                AllowBackup = bool.Parse(_data["AI_Settings"]["AllowBackup"]);
                TrafficStopDistance = float.Parse(_data["AI_Settings"]["TrafficStopDistance"]);
                PursuitTimeout = int.Parse(_data["AI_Settings"]["PursuitTimeout"]);
                BackupUnits = int.Parse(_data["AI_Settings"]["BackupUnits"]);

                EnableRoadblocks = bool.Parse(_data["Roadblocks"]["EnableRoadblocks"]);
                RoadblockFrequency = float.Parse(_data["Roadblocks"]["RoadblockFrequency"]);
                RoadblockDifficulty = _data["Roadblocks"]["RoadblockDifficulty"];

                EnableHelicopterSupport = bool.Parse(_data["Helicopters"]["EnableHelicopterSupport"]);
                HelicopterEngagementLevel = int.Parse(_data["Helicopters"]["HelicopterEngagementLevel"]);

                EnableSpikeStrips = bool.Parse(_data["SpikeStrips"]["EnableSpikeStrips"]);
                SpikeStripDeploymentLevel = int.Parse(_data["SpikeStrips"]["SpikeStripDeploymentLevel"]);

                EnableNotifications = bool.Parse(_data["Notifications"]["EnableNotifications"]);
                NotifyOnTrafficStop = bool.Parse(_data["Notifications"]["NotifyOnTrafficStop"]);
                NotifyOnPursuit = bool.Parse(_data["Notifications"]["NotifyOnPursuit"]);
                NotifyOnBackup = bool.Parse(_data["Notifications"]["NotifyOnBackup"]);

                EnableLogging = bool.Parse(_data["Logging"]["EnableLogging"]);
                CheckForUpdates = bool.Parse(_data["VersionCheck"]["CheckForUpdates"]);

                if (EnableLogging)
                {
                    Game.Console.Print("PoliceAI settings loaded successfully.");
                }
            }
            catch (Exception ex)
            {
                Game.Console.Print($"Error loading settings: {ex.Message}");
                Game.DisplayNotification("Error loading PoliceAI settings.");
            }
        }
    }
}
