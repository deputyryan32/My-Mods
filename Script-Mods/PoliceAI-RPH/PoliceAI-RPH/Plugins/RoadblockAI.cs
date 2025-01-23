using Rage;
using LSPD_First_Response.Mod.API;
using System;

namespace PoliceAI_RPH_Edition.Plugins
{
    public static class RoadblockAI
    {
        public static void Initialize()
        {
            if (ConfigManager.EnableLogging)
            {
                Game.Console.Print("Roadblock AI Initialized.");
            }

            if (ConfigManager.EnableRoadblocks)
            {
                Random random = new Random();

                GameFiber.StartNew(() =>
                {
                    while (Functions.GetActivePursuit() != null)
                    {
                        if (random.NextDouble() < ConfigManager.RoadblockFrequency)
                        {
                            Ped[] suspects = Functions.GetPursuitPeds(Functions.GetActivePursuit());
                            foreach (var suspect in suspects)
                            {
                                if (suspect && !suspect.IsDead)
                                {
                                    // AI places roadblock
                                    Functions.RequestBackup(suspect.Position, LSPD_First_Response.EBackupResponseType.Pursuit, LSPD_First_Response.EBackupUnitType.LocalUnit);
                                    Game.Console.Print("Roadblock placed for suspect.");
                                    Game.DisplayNotification("AI roadblock deployed against suspect.");
                                }
                            }
                        }

                        GameFiber.Sleep(15000);
                    }
                });
            }
        }
    }
}
