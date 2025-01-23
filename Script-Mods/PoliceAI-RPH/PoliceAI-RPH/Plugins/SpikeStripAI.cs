using Rage;
using LSPD_First_Response.Mod.API;

namespace PoliceAI_RPH_Edition.Plugins
{
    public static class SpikeStripAI
    {
        public static void Initialize()
        {
            if (ConfigManager.EnableLogging)
            {
                Game.Console.Print("SpikeStrip AI Initialized.");
            }

            if (ConfigManager.EnableSpikeStrips)
            {
                GameFiber.StartNew(() =>
                {
                    while (Game.LocalPlayer.WantedLevel >= ConfigManager.SpikeStripDeploymentLevel)
                    {
                        var pursuit = Functions.GetActivePursuit();
                        if (pursuit != null)
                        {
                            Ped[] suspects = Functions.GetPursuitPeds(pursuit);
                            foreach (var suspect in suspects)
                            {
                                if (suspect && !suspect.IsDead)
                                {
                                    Game.Console.Print("Spike strips deployed for suspect.");
                                    Game.DisplayNotification("AI spike strips deployed for suspect.");
                                }
                            }
                        }

                        GameFiber.Sleep(15000); // Wait before checking again
                    }
                });
            }
        }
    }
}
