using System.Linq;
using Rage;

namespace PoliceAI_RPH_Edition.Plugins
{
    public static class TrafficStopAI
    {
        public static void Initialize()
        {
            if (ConfigManager.EnableLogging)
            {
                Game.Console.Print("TrafficStop AI Initialized.");
            }

            if (ConfigManager.EnableTrafficStops)
            {
                float distance = ConfigManager.TrafficStopDistance;
                Game.DisplayHelp($"Traffic stops are enabled. Distance: {distance} meters.");

                GameFiber.StartNew(() =>
                {
                    while (true)
                    {
                        // Get all nearby entities and filter for vehicles
                        Entity[] nearbyEntities = World.GetEntities(Game.LocalPlayer.Character.Position, distance, GetEntitiesFlags.ConsiderAllVehicles);
                        Vehicle[] vehicles = nearbyEntities.OfType<Vehicle>().ToArray();

                        Vehicle closestVehicle = vehicles.OrderBy(v => v.DistanceTo(Game.LocalPlayer.Character.Position)).FirstOrDefault();

                        if (closestVehicle != null && Game.LocalPlayer.Character.DistanceTo(closestVehicle) < distance)
                        {
                            Game.Console.Print("AI has pulled over a vehicle for traffic stop.");
                            Game.DisplayNotification("AI Traffic Stop initiated.");
                        }

                        GameFiber.Sleep(5000);
                    }
                });
            }
        }
    }
}
