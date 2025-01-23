using GTA;
using GTA.Native;
using GTA.Math;

namespace PoliceAI_SHV.Plugins
{
    public static class SWATManager
    {
        public static void DeploySWAT(Vector3 playerPosition, int responseTime)
        {
            Vector3 spawnPosition = playerPosition.Around(100f);

            Vehicle swatVehicle = World.CreateVehicle(VehicleHash.Riot, spawnPosition);
            Ped swatOfficer1 = World.CreatePed(PedHash.Swat01SMY, swatVehicle.Position + new Vector3(1f, 0, 0));
            Ped swatOfficer2 = World.CreatePed(PedHash.Swat01SMY, swatVehicle.Position + new Vector3(-1f, 0, 0));

            swatOfficer1.Task.LeaveVehicle();
            swatOfficer2.Task.LeaveVehicle();
            swatOfficer1.Task.ShootAt(Game.Player.Character, -1);
            swatOfficer2.Task.ShootAt(Game.Player.Character, -1);
        }
    }
}
