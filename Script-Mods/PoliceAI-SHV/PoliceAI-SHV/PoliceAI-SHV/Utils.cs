using GTA;
using GTA.Native;
using GTA.Math;

namespace PoliceAI_SHV
{
    public static class Utils
    {
        // Spawn police at a specific position
        public static Ped SpawnPolice(Vector3 position)
        {
            Model policeModel = new Model(PedHash.Cop01SMY);
            policeModel.Request(500);  // Wait 500ms

            if (policeModel.IsLoaded)
            {
                Ped police = World.CreatePed(policeModel, position);
                return police;
            }
            return null;
        }
    }
}
