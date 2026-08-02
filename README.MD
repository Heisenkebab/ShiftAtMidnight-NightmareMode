# Nightmare Mode

An unofficial [BepInEx](https://github.com/BepInEx/BepInEx) mod for **Shift At Midnight** that makes the game meaner. Built for players who've cleared the base game and want the numbers to hurt.

## Features

- **Quota Scaling** — Daily store quotas are multiplied by 1.5x, starting from Day 1 and applying to every day after.

## Planned

- Increased entity (monster) damage, movement speed and HP
- Thieves can show up starting Day 1 and are also spawning more often, instead of only appearing later on.
- Vents become far less safe to hide in — you get kicked out of them much faster.
- Restock boxes only refill shelves with 5 items instead of 15.
- Tweakable multipliers (config values) for each of the above — damage, movement speed, HP, quota, etc. — instead of hardcoded amounts.
- Missing quota or dying wipes your save entirely, instead of just costing you the day.
- Customers are always on a time limit — no more infinite-patience shoppers.

## Requirements

- Shift At Midnight (Steam)
- [BepInEx 6 (IL2CPP)](https://github.com/BepInEx/BepInEx) installed for the game
- HarmonyX (bundled with BepInEx IL2CPP)

## Installation

1. Install BepInEx (IL2CPP) for Shift At Midnight if you haven't already.
2. Drop `NIGHTMAREMODE.dll` into `BepInEx/plugins/NIGHTMAREMODE/`.
3. Launch the game. Check `BepInEx/LogOutput.log` for `Plugin NIGHTMAREMODE is loaded!` to confirm it's active.

## Building from source

```
dotnet build
```

The project references the game's `Assembly-CSharp.dll`, `Fusion.Runtime.dll`, and `UnityEngine.CoreModule.dll` from a local `lib/` folder, which isn't committed to this repo — copy them there yourself from your own game install's interop/unhollowed assemblies.

On build, the project automatically copies the compiled DLL (and PDB) into your game's plugin folder. Update the `<GameDir>` property in `NIGHTMAREMODE.csproj` to point at your own install path first. 

## Disclaimer

This is an unofficial, fan-made mod and is not affiliated with or endorsed by the developers or publisher of Shift At Midnight.
