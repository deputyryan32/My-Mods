using GTA;
using GTA.Native;
using GTA.Math;

namespace PoliceAI_SHV.Plugins
{
    public static class CheckpointManager
    {
        public static void SetupCheckpoint(Vector3 playerPosition, float heading, int checkpointSpawnDistance)
        {
            // Find a location ahead of the player for a checkpoint
            Vector3 checkpointPosition = World.GetNextPositionOnStreet(playerPosition.Around(checkpointSpawnDistance));

            // Spawn police vehicles to block the road
            Vehicle policeCar1 = World.CreateVehicle(VehicleHash.Police, checkpointPosition + new Vector3(3f, 0, 0), heading);
            Vehicle policeCar2 = World.CreateVehicle(VehicleHash.Police, checkpointPosition + new Vector3(-3f, 0, 0), heading);

            // Spawn police officers to inspect vehicles and guard the checkpoint
            Ped officer1 = World.CreatePed(PedHash.Cop01SMY, checkpointPosition + new Vector3(2f, 0, 0));
            Ped officer2 = World.CreatePed(PedHash.Cop01SMY, checkpointPosition + new Vector3(-2f, 0, 0));

            officer1.Task.GuardCurrentPosition();
            officer2.Task.GuardCurrentPosition();
        }
    }
}
