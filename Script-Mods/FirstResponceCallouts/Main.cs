using FirstResponseCallouts.Logging;
using LSPD_First_Response.Mod.API;

namespace FirstResponseCallouts
{
    public class Main : Plugin
    {
        public override void Initialize()
        {
            Logger.Log("FirstResponseCallouts initializing...");
            Functions.OnOnDutyStateChanged += OnOnDutyStateChanged;
        }

        public override void Finally()
        {
            Logger.Log("FirstResponseCallouts shutting down...");
        }

        private void OnOnDutyStateChanged(bool onDuty)
        {
            if (onDuty)
            {
                Logger.Log("Player went on duty. Registering callouts...");
                Functions.RegisterCallout(typeof(Callouts.RobberyCallout));
                // Register additional callouts here
            }
        }
    }
}
