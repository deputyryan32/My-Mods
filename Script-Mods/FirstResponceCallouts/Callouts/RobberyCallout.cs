using System;
using Rage;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using FirstResponseCallouts.Logging;

namespace FirstResponseCallouts.Callouts
{
    [CalloutInfo("RobberyInProgress", CalloutProbability.High)]
    public class RobberyCallout : Callout
    {
        private Vector3 spawnPoint;
        private Ped suspect;

        public override bool OnBeforeCalloutDisplayed()
        {
            spawnPoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(300f));
            ShowCalloutAreaBlipBeforeAccepting(spawnPoint, 30f);
            CalloutMessage = "Robbery in Progress";
            CalloutPosition = spawnPoint;

            Logger.Log("Robbery Callout initialized at " + spawnPoint);
            return base.OnBeforeCalloutDisplayed();
        }

        public override bool OnCalloutAccepted()
        {
            suspect = new Ped(spawnPoint);
            suspect.Inventory.GiveNewWeapon("WEAPON_PISTOL", 50, true);
            Logger.Log("Suspect spawned with a pistol at " + spawnPoint);
            return base.OnCalloutAccepted();
        }

        public override void Process()
        {
            if (Game.IsKeyDown(System.Windows.Forms.Keys.End))
            {
                End();
            }
            base.Process();
        }

        public override void End()
        {
            Logger.Log("Robbery Callout ended.");
            if (suspect.Exists()) suspect.Dismiss();
            base.End();
        }
    }
}
