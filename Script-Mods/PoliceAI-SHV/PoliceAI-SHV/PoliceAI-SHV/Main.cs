using System;
using GTA;
using GTA.Native;
using GTA.Math;
using PoliceAI_SHV.Plugins;

namespace PoliceAI_SHV
{
    public class Main : Script
    {
        public Main()
        {
            // Load configuration settings
            Load.LoadConfig();

            // Set the tick function to run constantly
            Tick += OnTick;

            // Notify that the PoliceAI Script has been loaded
            GTA.UI.Notification.PostTicker("PoliceAI Script Loaded", false, true);
        }

        private void OnTick(object sender, EventArgs e)
        {
            var player = Game.Player.Character;

            if (Game.Player.WantedLevel > 0)
            {
                // Roadblocks
                if (Load.enableRoadblocks && World.GetDistance(player.Position, World.GetNextPositionOnStreet(player.Position)) < Load.responseDistance)
                {
                    GTA.UI.Screen.ShowSubtitle("Police are setting up a roadblock...", 5000);
                    RoadblockManager.CreateRoadblock(player.Position, player.Heading, Load.roadblockTimeout);
                }

                // Helicopters
                if (Load.enableHelicopters && Game.Player.WantedLevel >= 3)
                {
                    GTA.UI.Screen.ShowSubtitle("Helicopter inbound...", 5000);
                    HelicopterManager.SpawnHelicopter(player.Position + new Vector3(0, 0, Load.helicopterSpawnDistance), Load.helicopterResponseTime);
                }

                // SWAT
                if (Load.enableSWAT && Game.Player.WantedLevel == 5)
                {
                    GTA.UI.Screen.ShowSubtitle("SWAT Team incoming...", 5000);
                    SWATManager.DeploySWAT(player.Position, Load.swatResponseTime);
                }

                // Spike Strips
                if (Load.enableSpikeStrips && Game.Player.WantedLevel >= 3)
                {
                    GTA.UI.Screen.ShowSubtitle("Spike strips deployed!", 5000);
                    SpikeStripManager.DeploySpikeStrip(player.Position, Load.spikeStripResponseDistance);
                }

                // Snipers
                if (Load.enableSnipers && Game.Player.WantedLevel >= 4)
                {
                    GTA.UI.Screen.ShowSubtitle("Snipers deployed!", 5000);
                    SniperManager.DeploySnipers(player.Position, 3);

                }

                // Checkpoints
                if (Load.enableCheckpoints && Game.Player.WantedLevel >= 3)
                {
                    GTA.UI.Screen.ShowSubtitle("Police checkpoint ahead!", 5000);
                    CheckpointManager.SetupCheckpoint(player.Position, player.Heading, Load.checkpointSpawnDistance);
                }
            }
        }
    }
}
