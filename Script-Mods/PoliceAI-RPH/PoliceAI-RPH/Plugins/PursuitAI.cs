using Rage;
using LSPD_First_Response.Mod.API;

namespace PoliceAI_RPH_Edition.Plugins
{
    public static class PursuitAI
    {
        public static void Initialize()
        {
            if (ConfigManager.EnableLogging)
            {
                Game.Console.Print("Pursuit AI Initialized.");
            }

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
                                Game.Console.Print("AI officers are engaging the suspect.");
                                Game.DisplayNotification("AI pursuing suspect.");

                                GameFiber.Sleep(ConfigManager.PursuitTimeout * 1000);
                                Game.Console.Print("Pursuit timed out for suspect.");
                                Game.DisplayNotification("AI Pursuit timed out.");
                            }
                        }
                    }

                    GameFiber.Sleep(10000);
                }
            });
        }
    }
}
