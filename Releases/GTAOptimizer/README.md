# GTAOptimizer

**Created by DeputyRyan and DeputyMods**

---

## Overview

GTAOptimizer is a performance-enhancing mod for GTA V, tailored for players seeking smoother gameplay on low-end PCs. By analyzing system specifications and adjusting game settings every second, GTAOptimizer delivers optimized gameplay, reducing lag and improving frame rates without additional configurations. The mod utilizes SHVDN3 Nightly to access GTA V’s density and draw distance settings, dynamically adjusting based on your current FPS.

## How It Works

Upon installation, GTAOptimizer performs a one-time system specification detection, then adjusts game settings based on real-time FPS data to provide the best balance between performance and visual quality. Here’s a breakdown of its key functions:

1. **System Detection**: GTAOptimizer identifies the CPU core count, available RAM (capped at 64GB), and GPU model once during initialization. This information is used to apply an initial optimization level tailored to your hardware.

2. **Real-Time FPS Monitoring**: Frames per second (FPS) are calculated every 30 seconds, and the mod adjusts settings based on the following thresholds:
   - **Low-End Optimizations**: Activated if FPS drops below 30. Reduces pedestrian and vehicle density, lowers texture resolution, and shortens draw distance.
   - **Medium Optimizations**: Enabled if FPS is between 30 and 50, applying balanced settings for density, texture, and draw distance.
   - **High-End Optimizations**: Used when FPS exceeds 50, providing a fuller game experience with moderate density and higher texture quality.

3. **Log File**: GTAOptimizer logs every adjustment and action taken to a `GTAOptimizer.txt` file in your GTA V directory. This log tracks system specs, FPS updates, and every optimization applied, providing a clear record of the mod’s actions.

## Installation

1. **Drag and Drop**: Move all files in the `Grand Theft Auto V` folder to your **Grand Theft Auto V** directory.
2. **Auto-Sort**: Windows’ auto-sort will automatically organize the files for you, completing the installation.

## System Requirements

- **Compatibility**: GTAOptimizer is compatible with SHVDN3 Nightly.
- **Visual C++ Redistributable**: Ensure the Visual C++ Redistributable is installed, as required by SHVDN3.

## Usage

GTAOptimizer will automatically run every second to apply optimal settings based on your system’s performance. Enjoy a smoother GTA V experience tailored to your hardware and current FPS levels.

---
