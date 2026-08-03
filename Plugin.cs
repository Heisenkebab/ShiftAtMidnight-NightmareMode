using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using ModSettingsMenu.Api;
using UnityEngine;

namespace NIGHTMAREMODE;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
[BepInDependency(ModSettingsMenu.PluginInfo.PLUGIN_GUID, BepInDependency.DependencyFlags.SoftDependency)]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;

    public override void Load()
    {
        // Plugin startup logic
        Log = base.Log;
        var harmony = new Harmony("net.heisenkebab.nightmaremode");
        harmony.PatchAll();

        registerConfig();

        //Register ThiefSpawnTimer
        ClassInjector.RegisterTypeInIl2Cpp<ThiefSpawnTimer>();
        var thiefTimerObj = new GameObject("NightmareModeThiefTimer");
        GameObject.DontDestroyOnLoad(thiefTimerObj);
        thiefTimerObj.AddComponent<ThiefSpawnTimer>();

        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

    }

    private void registerConfig()
    {
        NightmareSettings.Enabled = Config.Bind("General", "Enabled", true, "Enable this mod.");

        NightmareSettings.QuotaScaling = Config.Bind("Quota", "Quota Scaling", 1.5f, new ConfigDescription("Adjust the quota multiplier", new AcceptableValueRange<float>(1f, 10f)));
        NightmareSettings.QuotaScaling.SettingChanged += (object sender, EventArgs e) => NightmareSettings.QuotaScaling.Value = Mathf.Round(NightmareSettings.QuotaScaling.Value * 10f) / 10f;

        NightmareSettings.ShouldSpawnThieves = Config.Bind("Thieves", "Should Spawn Thieves", true, "Enable or disable thief spawns.");

        NightmareSettings.SpawnInterval = Config.Bind("Thieves", "Spawn Interval", 30, new ConfigDescription("Adjust the spawn interval for thieves (seconds)", new AcceptableValueRange<int>(5, 60)));

        NightmareSettings.SpawnChance = Config.Bind("Thieves", "Spawn Chance", 0.5f, new ConfigDescription("Adjust the spawn chance for thieves", new AcceptableValueRange<float>(0f, 1f)));
        NightmareSettings.SpawnChance.SettingChanged += (object sender, EventArgs e) =>
        NightmareSettings.SpawnChance.Value = Mathf.Round(NightmareSettings.SpawnChance.Value * 10f) / 10f;

        NightmareSettings.DamageScaling = Config.Bind("Damage", "Damage Scaling", 5f, new ConfigDescription("Adjust the damage multiplier", new AcceptableValueRange<float>(1f, 10f)));
        NightmareSettings.DamageScaling.SettingChanged += (object sender, EventArgs e) => NightmareSettings.DamageScaling.Value = Mathf.Round(NightmareSettings.DamageScaling.Value * 10f) / 10f;

        ModSettingsRegistry.Register(
          PluginInfo.PLUGIN_GUID,
          new ModSettingsModOptions
          {
              Name = "NIGHTMARE Mode",
              Description = "A mod that makes the game a nightmare",
              Author = "Heisenkebab",
              Version = PluginInfo.PLUGIN_VERSION,
              NexusModsId = 6,
              ThunderstoreTeam = "Heisenkebab",
              ThunderstoreModName = "NIGHTMAREMode"
          });
    }
}
