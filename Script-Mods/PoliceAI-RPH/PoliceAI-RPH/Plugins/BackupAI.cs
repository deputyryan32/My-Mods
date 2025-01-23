using Rage;
using LSPD_First_Response.Mod.API;

namespace PoliceAI_RPH_Edition.Plugins
{
    public static class BackupAI
    {
        public static void Initialize()
        {
            if (ConfigManager.EnableLogging)
            {
                Game.Console.Print("Backup AI Initialized.");
            }

            if (ConfigManager.AllowBackup)
            {
                GameFiber.StartNew(() =>
                {
                    while (true)
                    {
                        var pursuit = Functions.GetActivePursuit();
                        if (pursuit != null)
                        {
                            Ped[] suspects = Functions.GetPursuitPeds(pursuit);

                            foreach (var suspect in suspects)
                            {
                                if (suspect && !suspect.IsDead)
                                {
                                    for (int i = 0; i < ConfigManager.BackupUnits; i++)
                                    {
                                        Functions.RequestBackup(suspect.Position, LSPD_First_Response.EBackupResponseType.Pursuit, LSPD_First_Response.EBackupUnitType.LocalUnit);
                                        Game.Console.Print("Backup units are called for suspect.");
                                    }

                                    Game.DisplayNotification("Backup requested for pursuit suspect.");
                                }
                            }
                        }

                        GameFiber.Sleep(10000);
                    }
                });
            }
        }
    }
}
