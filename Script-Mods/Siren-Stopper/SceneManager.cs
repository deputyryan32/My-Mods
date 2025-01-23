using Rage;
using System;
using System.Linq;

namespace SirenStopper
{
    public static class SceneManager
    {
        private static readonly Random random = new Random();

        public static void MonitorPoliceVehicles()
        {
            Game.LogTrivial("Siren Stopper: Monitoring police vehicles.");

            while (true)
            {
                GameFiber.Yield();

                var policeVehicles = World.GetAllVehicles()
                    .Where(v => v.Exists() && v.IsPoliceVehicle && v.Driver != null && !v.Driver.IsPlayer && v.IsSirenOn);

                foreach (var vehicle in policeVehicles)
                {
                    if (IsSceneCode4() && ShouldAIHandleSiren())
                    {
                        GameFiber.StartNew(() => HandleSiren(vehicle));
                    }
                }
            }
        }

        private static bool IsSceneCode4()
        {
            // Implement logic to determine if the scene is code 4
            // For example, check if there are no nearby suspects or ongoing pursuits
            // This is a placeholder and should be replaced with actual logic
            return true;
        }

        private static bool ShouldAIHandleSiren()
        {
            // Introduce randomness to make behavior less frequent
            return random.NextDouble() < 0.5; // 50% chance
        }

        private static void HandleSiren(Vehicle vehicle)
        {
            if (!vehicle.Exists() || vehicle.Driver == null) return;

            Ped driver = vehicle.Driver;

            if (!driver.IsInAnyVehicle(false))
            {
                Game.LogTrivial($"Siren Stopper: AI officer returning to vehicle {vehicle.Model.Name} to disable siren.");
                driver.Tasks.EnterVehicle(vehicle, -1).WaitForCompletion(); // -1 represents the driver seat
            }

            if (vehicle.IsSirenOn)
            {
                vehicle.IsSirenOn = false;
                Game.LogTrivial($"Siren Stopper: Siren turned off for vehicle {vehicle.Model.Name}.");
            }
        }
    }
}
