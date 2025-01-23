using GTA;
using GTA.Native;
using GTA.Math;
using System.Collections.Generic;

namespace PoliceAI_SHV.Plugins
{
    public static class SniperManager
    {
        // Settings for sniper behavior
        public static int sniperSpawnDistance = 100;   // Distance from player to spawn snipers
        public static float rooftopHeightThreshold = 10.0f;  // Minimum height to be considered a rooftop

        public static void DeploySnipers(Vector3 playerPosition, int sniperCount)
        {
            // Find a rooftop or high ground near the player
            List<Vector3> potentialRooftops = FindNearbyRooftops(playerPosition, sniperSpawnDistance);

            if (potentialRooftops.Count > 0)
            {
                for (int i = 0; i < sniperCount && i < potentialRooftops.Count; i++)
                {
                    Vector3 rooftop = potentialRooftops[i];
                    Ped sniper = World.CreatePed(PedHash.Cop01SMY, rooftop);
                    sniper.Weapons.Give(WeaponHash.SniperRifle, 100, true, true);
                    sniper.Task.AimGunAtEntity(Game.Player.Character, -1);  // Engage player from rooftop
                }
            }
        }

        // Find elevated positions (like rooftops) near the player within a certain radius
        private static List<Vector3> FindNearbyRooftops(Vector3 playerPosition, float searchRadius)
        {
            List<Vector3> rooftops = new List<Vector3>();
            for (int i = 0; i < 10; i++)
            {
                Vector3 potentialPosition = World.GetNextPositionOnSidewalk(playerPosition.Around(searchRadius));

                // Check if the position is elevated to be considered a rooftop
                float groundHeight = World.GetGroundHeight(potentialPosition);
                if (potentialPosition.Z - groundHeight >= rooftopHeightThreshold)
                {
                    rooftops.Add(potentialPosition);
                }
            }

            return rooftops;
        }
    }
}
