using GTA;
using GTA.Native;
using GTA.Math;

namespace PoliceAI_SHV.Plugins
{
    public static class RoadblockManager
    {
        public static void CreateRoadblock(Vector3 playerPosition, float heading, int roadblockTimeout)
        {
            Vector3 roadblockPosition = World.GetNextPositionOnStreet(playerPosition.Around(100f));

            Vehicle policeCar1 = World.CreateVehicle(VehicleHash.Police, roadblockPosition + new Vector3(3f, 0, 0), heading);
            Vehicle policeCar2 = World.CreateVehicle(VehicleHash.Police, roadblockPosition + new Vector3(-3f, 0, 0), heading);

            Prop barrier1 = World.CreateProp(new Model("prop_barrier_work05"), roadblockPosition + new Vector3(5f, 0, 0), true, false);
            Prop barrier2 = World.CreateProp(new Model("prop_barrier_work05"), roadblockPosition + new Vector3(-5f, 0, 0), true, false);

            Ped officer1 = World.CreatePed(PedHash.Cop01SMY, roadblockPosition + new Vector3(2f, 0, 0));
            Ped officer2 = World.CreatePed(PedHash.Cop01SMY, roadblockPosition + new Vector3(-2f, 0, 0));

            officer1.Task.AimGunAtEntity(Game.Player.Character, -1);
            officer2.Task.AimGunAtEntity(Game.Player.Character, -1);

            // Roadblock will last for the duration specified in the INI file
            Script.Wait(roadblockTimeout);
            policeCar1.Delete();
            policeCar2.Delete();
            barrier1.Delete();
            barrier2.Delete();
            officer1.Delete();
            officer2.Delete();
        }
    }
}
