using GTA;
using GTA.Native;

namespace PoliceAI_SHV
{
    public static class AIController
    {
        // How police engage the player during a chase
        public static void EngagePlayerInChase(Ped police, Ped player, bool aggressive)
        {
            if (aggressive)
            {
                // Make the police more aggressive
                Function.Call(Hash.TASK_VEHICLE_CHASE, police, player);
            }
            else
            {
                Function.Call(Hash.TASK_FOLLOW_TO_OFFSET_OF_ENTITY, police, player, 0, 0, 0, 10.0f, -1, 10.0f, true);
            }
        }
    }
}
