using GTA;
using GTA.Native;
using GTA.Math;

namespace PoliceAI_SHV.Plugins
{
    public static class HelicopterManager
    {
        public static void SpawnHelicopter(Vector3 spawnPosition, int responseTime)
        {
            Vehicle policeHelicopter = World.CreateVehicle(VehicleHash.Polmav, spawnPosition);
            Ped helicopterPilot = policeHelicopter.CreatePedOnSeat(VehicleSeat.Driver, PedHash.Pilot01SMY);

            Function.Call(Hash.TASK_HELI_MISSION, helicopterPilot, policeHelicopter, 0, 0, Game.Player.Character.Position.X, Game.Player.Character.Position.Y, Game.Player.Character.Position.Z, 6, 20.0f, -1.0f, -1.0f, 10, 10, -1.0f, 0);
        }
    }
}
