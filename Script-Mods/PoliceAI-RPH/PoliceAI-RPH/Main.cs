using System;
using Rage;
using LSPD_First_Response.Mod.API;

namespace PoliceAI_RPH_Edition
{
    public class Main : Plugin
    {
        public override void Initialize()
        {
            if (ConfigManager.EnableLogging)
            {
                Game.Console.Print("PoliceAI RPH Edition v1.2.0 Initialized by DeputyRyan.");
            }

            if (ConfigManager.EnableNotifications)
            {
                Game.DisplayNotification("PoliceAI RPH Edition v1.2.0 Initialized.");
            }

            ConfigManager.LoadSettings();

            Plugins.TrafficStopAI.Initialize();
            Plugins.PursuitAI.Initialize();
            Plugins.BackupAI.Initialize();
            Plugins.RoadblockAI.Initialize();
            Plugins.HelicopterAI.Initialize();
            Plugins.SpikeStripAI.Initialize();

            if (ConfigManager.CheckForUpdates)
            {
                VersionCheck.VersionChecker.CheckForUpdates();
            }
        }

        public override void Finally()
        {
            if (ConfigManager.EnableLogging)
            {
                Game.Console.Print("PoliceAI RPH Edition v1.2.0 Terminated.");
            }

            if (ConfigManager.EnableNotifications)
            {
                Game.DisplayNotification("PoliceAI RPH Edition v1.2.0 Terminated.");
            }
        }
    }
}
