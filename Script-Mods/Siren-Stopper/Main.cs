using Rage;
using LSPD_First_Response.Mod.API;

[assembly: Rage.Attributes.Plugin("Siren Stopper", Description = "A plugin to manage AI sirens after scenes are code 4.", Author = "DeputyRyan, DeputyMods")]

namespace SirenStopper
{
    public class Main : Plugin
    {
        public override void Initialize()
        {
            Game.LogTrivial("Siren Stopper: Plugin initialized.");
            Functions.OnOnDutyStateChanged += OnOnDutyStateChanged;
        }

        public override void Finally()
        {
            Game.LogTrivial("Siren Stopper: Plugin cleaned up.");
        }

        private void OnOnDutyStateChanged(bool onDuty)
        {
            if (onDuty)
            {
                Game.DisplayNotification("~b~Siren Stopper ~w~is now active.");
                GameFiber.StartNew(SceneManager.MonitorPoliceVehicles);
            }
        }
    }
}
