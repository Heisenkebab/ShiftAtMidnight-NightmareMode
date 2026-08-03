# Nightmare Mode

An unofficial [BepInEx](https://github.com/BepInEx/BepInEx) mod for **Shift At Midnight** that makes the game meaner. Built for players who've cleared the base game and want the numbers to hurt.

## Features

- **Quota Scaling** — Daily store quotas are multiplied by 1.5x, starting from Day 1 and applying to every day after.
- Thieves can show up starting Day 1 and are also spawning more often, instead of only appearing later on.
- Increase spider movement speed and HP
- Damage taken by the player is increased

## Planned

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

The project references the following DLLs from a local `lib/` folder, which isn't committed to this repo — copy them there yourself from your own game install's interop/unhollowed assemblies:

- `Assembly-CSharp.dll`
- `Fusion.Runtime.dll`
- `Il2Cppmscorlib.dll`
- `ModSettingsMenu.dll`
- `UnityEngine.CoreModule.dll`
- `UnityEngine.UI.dll`

On build, the project automatically copies the compiled DLL into your game's plugin folder. Update the `<GameDir>` property in `NIGHTMAREMODE.csproj` to point at your own install path first. Otherwise, the built `NIGHTMAREMODE.dll` can be found in `bin/Debug/net6.0/`.

## Disclaimer

This is an unofficial, fan-made mod and is not affiliated with or endorsed by the developers or publisher of Shift At Midnight.
