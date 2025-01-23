using System;
using System.Collections.Generic;
using GTA;
using GTA.Native;
using GTA.Math;

public class SecondAmendmentNPCs : Script
{
    private Random random = new Random();

    public SecondAmendmentNPCs()
    {
        Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        // Get all nearby pedestrians in a certain range
        var nearbyPeds = World.GetNearbyPeds(Game.Player.Character.Position, 100.0f);

        foreach (var ped in nearbyPeds)
        {
            if (!ped.IsPlayer && ped.IsAlive && !ped.IsPersistent)
            {
                // Check if the NPC already has a weapon
                if (!Function.Call<bool>(Hash.HAS_PED_GOT_WEAPON, ped.Handle, WeaponHash.Pistol, false))
                {
                    // Give the NPC a random firearm
                    WeaponHash weapon = GetRandomFirearm();
                    Function.Call(Hash.GIVE_WEAPON_TO_PED, ped.Handle, weapon, 100, false, true);
                }

                // Set combat attributes for NPCs to defend themselves if attacked
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped.Handle, 46, true);  // Allow combat if attacked
                Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, ped.Handle, 5, true);   // Always fight back when threatened
            }
        }
    }

    private WeaponHash GetRandomFirearm()
    {
        WeaponHash[] firearms = { WeaponHash.Pistol, WeaponHash.MicroSMG, WeaponHash.PumpShotgun };
        return firearms[random.Next(firearms.Length)];
    }
}
