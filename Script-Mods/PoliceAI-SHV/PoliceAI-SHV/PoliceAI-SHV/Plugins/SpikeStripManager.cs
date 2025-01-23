using GTA;
using GTA.Native;
using GTA.Math;

namespace PoliceAI_SHV.Plugins
{
    public static class SpikeStripManager
    {
        public static void DeploySpikeStrip(Vector3 playerPosition, int spikeStripResponseDistance)
        {
            // Find a position ahead of the player's vehicle for spike strip deployment
            Vector3 spikeStripPosition = World.GetNextPositionOnStreet(playerPosition.Around(spikeStripResponseDistance));

            // Spawn the spike strip object
            Prop spikeStrip = World.CreateProp(new Model("p_ld_stinger_s"), spikeStripPosition, true, true);

            // Spike strips last for 15 seconds
            Script.Wait(15000);
            spikeStrip.Delete();
        }
    }
}
