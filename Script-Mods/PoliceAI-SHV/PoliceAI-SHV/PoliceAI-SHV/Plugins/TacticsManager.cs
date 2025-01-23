using GTA;
using GTA.Native;
using GTA.Math;

namespace PoliceAI_SHV.Plugins
{
    public static class TacticsManager
    {
        public static void FlankPlayer(Ped policeOfficer, Vector3 playerPosition)
        {
            Vector3 flankPosition = playerPosition.Around(10f);
            policeOfficer.Task.RunTo(flankPosition);
        }

        public static void CallReinforcements(Vector3 callLocation)
        {
            Ped backupOfficer1 = World.CreatePed(PedHash.Cop01SMY, callLocation.Around(5f));
            Ped backupOfficer2 = World.CreatePed(PedHash.Cop01SMY, callLocation.Around(-5f));

            backupOfficer1.Task.FollowNavMeshTo(Game.Player.Character.Position);
            backupOfficer2.Task.FollowNavMeshTo(Game.Player.Character.Position);
        }
    }
}
