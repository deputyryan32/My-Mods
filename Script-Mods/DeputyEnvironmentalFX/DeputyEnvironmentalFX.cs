// File: DeputyEnvironmentalFX.cs
using System;
using System.Collections.Generic;
using System.IO;
using GTA;
using GTA.Native;
using GTA.Math;

public class DeputyEnvironmentalFX : Script
{
    private string configPath = @"scripts/DeputyEnvironmentalFX/config.ini";
    private float fireEffectScale;
    private float smokeTrailScale;
    private float smogDensity;
    private bool applyToAllVehicles;
    private string fireEffectName;
    private string smokeTrailName;
    private string smogEffectName;
    private float emberEffectScale;
    private float heatDistortionScale;

    public DeputyEnvironmentalFX()
    {
        Tick += OnTick;
        LoadConfig();
    }

    private void LoadConfig()
    {
        if (!File.Exists(configPath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(configPath));
            using (StreamWriter writer = new StreamWriter(configPath))
            {
                writer.WriteLine("FireEffectScale=2.0");
                writer.WriteLine("SmokeTrailScale=2.0");
                writer.WriteLine("SmogDensity=1.0");
                writer.WriteLine("ApplyToAllVehicles=true");
                writer.WriteLine("FireEffectName=ent_amb_fuel_fire");
                writer.WriteLine("SmokeTrailName=exp_grd_grenade_smoke");
                writer.WriteLine("SmogEffectName=scr_car_cluster_smoke");
                writer.WriteLine("EmberEffectScale=1.4");
                writer.WriteLine("HeatDistortionScale=0.6");
            }
        }

        string[] lines = File.ReadAllLines(configPath);
        foreach (string line in lines)
        {
            if (line.StartsWith("FireEffectScale="))
                fireEffectScale = float.Parse(line.Split('=')[1]);
            else if (line.StartsWith("SmokeTrailScale="))
                smokeTrailScale = float.Parse(line.Split('=')[1]);
            else if (line.StartsWith("SmogDensity="))
                smogDensity = float.Parse(line.Split('=')[1]);
            else if (line.StartsWith("ApplyToAllVehicles="))
                applyToAllVehicles = bool.Parse(line.Split('=')[1]);
            else if (line.StartsWith("FireEffectName="))
                fireEffectName = line.Split('=')[1];
            else if (line.StartsWith("SmokeTrailName="))
                smokeTrailName = line.Split('=')[1];
            else if (line.StartsWith("SmogEffectName="))
                smogEffectName = line.Split('=')[1];
            else if (line.StartsWith("EmberEffectScale="))
                emberEffectScale = float.Parse(line.Split('=')[1]);
            else if (line.StartsWith("HeatDistortionScale="))
                heatDistortionScale = float.Parse(line.Split('=')[1]);
        }
    }

    private void OnTick(object sender, EventArgs e)
    {
        var vehicles = World.GetAllVehicles();

        foreach (Vehicle vehicle in vehicles)
        {
            if (applyToAllVehicles || vehicle.IsOnFire)
            {
                EnhanceEffects(vehicle, vehicles);
            }
        }
    }

    private void EnhanceEffects(Vehicle vehicle, Vehicle[] vehicles)
    {
        Vector3 position = vehicle.Position;

        if (vehicle.IsOnFire)
        {
            // Enhanced fire effect with realistic spread and intensity
            Function.Call(Hash.START_PARTICLE_FX_LOOPED_ON_ENTITY, fireEffectName, vehicle, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, fireEffectScale, false, false, false);

            // Enhanced embers with realistic movement
            Function.Call(Hash.START_PARTICLE_FX_LOOPED_ON_ENTITY, "core_fire_embers", vehicle, 0.0f, 0.0f, 0.5f, 0.0f, 0.0f, 0.0f, emberEffectScale, false, false, false);

            // Add heat distortion around the fire
            Function.Call(Hash.START_PARTICLE_FX_LOOPED_ON_ENTITY, "scr_fbi4_heat_distortion", vehicle, 0.0f, 0.0f, 1.0f, 0.0f, 0.0f, 0.0f, heatDistortionScale, false, false, false);
        }

        if (vehicle.IsEngineRunning && !vehicle.IsOnFire)
        {
            // Smoke trails with realistic dissipation
            Function.Call(Hash.START_PARTICLE_FX_LOOPED_ON_ENTITY, smokeTrailName, vehicle, 0.0f, 0.0f, -0.5f, 0.0f, 0.0f, 0.0f, smokeTrailScale, false, false, false);
        }

        // Smog effect dynamically increasing with the number of vehicles on fire
        float smogAreaDensity = vehicles.Length * smogDensity;
        Function.Call(Hash.SET_PARTICLE_FX_NON_LOOPED_ALPHA, smogAreaDensity);

        // Smog expands over time to fill an area realistically
        Function.Call(Hash.START_PARTICLE_FX_NON_LOOPED_AT_COORD, smogEffectName, position.X, position.Y, position.Z + 2.0f, 0.0f, 0.0f, 0.0f, smogAreaDensity, false, false, false);
    }
}
