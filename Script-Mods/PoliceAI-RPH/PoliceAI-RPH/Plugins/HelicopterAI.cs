using Rage;
using LSPD_First_Response.Mod.API;

namespace PoliceAI_RPH_Edition.Plugins
{
    public static class HelicopterAI
    {
        public static void Initialize()
        {
            if (ConfigManager.EnableLogging)
            {
                Game.Console.Print("Helicopter AI Initialized.");
            }

            if (ConfigManager.EnableHelicopterSupport)
            {
                GameFiber.StartNew(() =>
                {
                    while (Game.LocalPlayer.WantedLevel >= ConfigManager.HelicopterEngagementLevel)
                    {
                        var pursuit = Functions.GetActivePursuit();
                        if (pursuit != null)
                        {
                            Ped[] suspects = Functions.GetPursuitPeds(pursuit);

                            foreach (var suspect in suspects)
                            {
                                if (suspect && !suspect.IsDead)
                                {
                                    Functions.RequestBackup(suspect.Position, LSPD_First_Response.EBackupResponseType.Pursuit, LSPD_First_Response.EBackupUnitType.AirUnit);
                                    Game.Console.Print("Helicopter engaged for suspect.");
                                    Game.DisplayNotification("Helicopter support dispatched for suspect.");
                                }
                            }
                        }

                        GameFiber.Sleep(60000);
                    }
                });
            }
        }
    }
}
