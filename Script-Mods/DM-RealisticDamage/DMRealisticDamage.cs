using GTA;
using GTA.Native;
using GTA.Math;
using GTA.UI;
using System;
using System.Collections.Generic;
using Hash = GTA.Native.Hash;

public class DMRealisticDamage : Script
{
    // Configurable parameters
    private const float HeadshotMultiplier = 10.0f;
    private const float LimbDamageMultiplier = 0.3f;
    private const float TorsoDamageMultiplier = 1.0f;
    private const float CriticalTorsoMultiplier = 2.0f;
    private const float NPCHealthMultiplier = 0.75f; // Reduce NPC health for realism
    private const float BleedingDamageRate = 2.0f; // Damage per second from bleeding
    private const float KnockbackForce = 5.0f; // Knockback force from gun damage
    private const float BoneBreakDamageThreshold = 30.0f; // Damage threshold for breaking bones

    private Dictionary<Ped, DateTime> BleedingPeds = new Dictionary<Ped, DateTime>();
    private Dictionary<Ped, HashSet<int>> BrokenBones = new Dictionary<Ped, HashSet<int>>();
    private Dictionary<Ped, int> LastHealth = new Dictionary<Ped, int>();

    public DMRealisticDamage()
    {
        Tick += OnTick;
    }

    private void OnTick(object sender, EventArgs e)
    {
        foreach (Ped ped in World.GetAllPeds())
        {
            if (ped.IsHuman && ped.IsAlive)
            {
                TrackHealth(ped);
                ApplyLocationalDamage(ped);
                ApplyBleedingEffect(ped);
                ApplyBrokenBoneEffects(ped);
                ApplyScreenEffects(ped);
                ApplyShockEffects(ped);
            }
        }

        ApplySpecialRagdoll(Game.Player.Character);
        ApplyBrokenBoneEffects(Game.Player.Character);
        ApplyScreenEffects(Game.Player.Character);
        ApplyShockEffects(Game.Player.Character);
    }

    private void TrackHealth(Ped ped)
    {
        if (!LastHealth.ContainsKey(ped))
        {
            LastHealth[ped] = ped.Health;
        }

        if (ped.Health < LastHealth[ped])
        {
            LastHealth[ped] = ped.Health;
        }
    }

    private void ApplyLocationalDamage(Ped ped)
    {
        if (!LastHealth.ContainsKey(ped)) return;

        int damageTaken = LastHealth[ped] - ped.Health;
        if (damageTaken <= 0) return;

        int hitBone = GetRandomBoneId(); // Simulating bone hit

        float damageMultiplier = GetDamageMultiplier(hitBone);

        ped.ApplyDamage((int)(damageTaken * damageMultiplier));
        ApplyKnockback(ped, damageTaken);

        if (damageTaken >= BoneBreakDamageThreshold)
        {
            BreakBone(ped, hitBone);
        }

        if (!BleedingPeds.ContainsKey(ped) && damageMultiplier > 1.0f)
        {
            BleedingPeds.Add(ped, DateTime.Now);
        }
    }

    private void BreakBone(Ped ped, int boneId)
    {
        if (!BrokenBones.ContainsKey(ped))
        {
            BrokenBones[ped] = new HashSet<int>();
        }

        if (!BrokenBones[ped].Contains(boneId))
        {
            BrokenBones[ped].Add(boneId);
            Function.Call(Hash.PLAY_SOUND_FROM_ENTITY, -1, "BREAK", ped, "DLC_HEIST_BIOLAB_PREP_HACKING_SOUNDS", true, 0);

            // Display notification
            if (ped.IsPlayer)
            {
                Notify($"You broke a bone (ID: {boneId}).");
            }
        }
    }

    private void Notify(string message)
    {
        Notification.Show(message);
    }

    private void ApplyBrokenBoneEffects(Ped ped)
    {
        if (BrokenBones.ContainsKey(ped))
        {
            if (BrokenBones[ped].Contains(31086) || BrokenBones[ped].Contains(11816)) // Example: leg bones
            {
                ped.Task.PlayAnimation("move_crawl", "onfront_fwd", 8.0f, -8.0f, -1, AnimationFlags.Loop, 0);
            }

            if (BrokenBones[ped].Contains(28252)) // Example: right arm bone
            {
                Weapon currentWeapon = ped.Weapons.Current;
                if (currentWeapon != null)
                {
                    ped.Weapons.Remove(currentWeapon);
                }
            }
        }
    }

    private void ApplyKnockback(Ped ped, float damage)
    {
        Vector3 direction = (ped.Position - Game.Player.Character.Position).Normalized;
        ped.ApplyForce(direction * KnockbackForce * (damage / 50.0f));
    }

    private void ApplyBleedingEffect(Ped ped)
    {
        if (BleedingPeds.ContainsKey(ped))
        {
            TimeSpan elapsed = DateTime.Now - BleedingPeds[ped];
            if (elapsed.TotalSeconds >= 1)
            {
                ped.Health -= (int)BleedingDamageRate;
                BleedingPeds[ped] = DateTime.Now;

                Function.Call(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, "blood_impact", ped, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 1.0f, false, false, false);
            }

            if (ped.Health <= 0 || !ped.IsAlive)
            {
                BleedingPeds.Remove(ped);
            }
        }
    }

    private void ApplyScreenEffects(Ped ped)
    {
        if (ped.IsPlayer && ped.Health < 50)
        {
            Function.Call(Hash.ANIMPOSTFX_PLAY, "FocusOut", 0, true);
        }
        else if (ped.IsPlayer && ped.Health >= 50)
        {
            Function.Call(Hash.ANIMPOSTFX_STOP, "FocusOut");
        }

        if (BleedingPeds.ContainsKey(ped))
        {
            Function.Call(Hash.ANIMPOSTFX_PLAY, "RedMist", 0, true);
        }
    }

    private void ApplyShockEffects(Ped ped)
    {
        if (ped.Health < 30 && ped.IsPlayer)
        {
            Function.Call(Hash.SHAKE_GAMEPLAY_CAM, "SMALL_EXPLOSION_SHAKE", 0.3f);
        }
    }

    private void ApplySpecialRagdoll(Ped ped)
    {
        if (ped.IsAlive && LastHealth.ContainsKey(ped) && ped.Health < LastHealth[ped])
        {
            ped.Ragdoll();
        }
    }

    private float GetDamageMultiplier(int boneId)
    {
        switch (boneId)
        {
            case 12844: // Head bone
                return HeadshotMultiplier;
            case 28252: // Right hand
            case 57005: // Left hand
            case 21030: // Left foot
            case 65245: // Right foot
                return LimbDamageMultiplier;
            case 24818: // Spine
                return CriticalTorsoMultiplier;
            default:
                return TorsoDamageMultiplier; // Default multiplier
        }
    }

    private int GetRandomBoneId()
    {
        int[] boneIds = { 12844, 28252, 57005, 21030, 65245, 24818 };
        Random rand = new Random();
        return boneIds[rand.Next(boneIds.Length)];
    }

    private float CalculateGeneralDamage(Ped ped)
    {
        Weapon weapon = Game.Player.Character.Weapons.Current;

        if (weapon != null)
        {
            return 50.0f; // Approximate damage value based on weapon type
        }

        return 10.0f; // Default fallback damage
    }
}
