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

        registerConfig();

        //Register ThiefSpawnTimer
        ClassInjector.RegisterTypeInIl2Cpp<ThiefSpawnTimer>();
        GameObject thiefTimerObj = new GameObject("NightmareModeThiefTimer");
        GameObject.DontDestroyOnLoad(thiefTimerObj);
        thiefTimerObj.AddComponent<ThiefSpawnTimer>();

        //Register NightmareRunEnder. Must survive the scene unload it triggers.
        ClassInjector.RegisterTypeInIl2Cpp<NightmareRunEnder>();
        GameObject runEnderObj = new GameObject("NightmareModeRunEnder");
        GameObject.DontDestroyOnLoad(runEnderObj);
        NightmareRunEnder.Instance = runEnderObj.AddComponent<NightmareRunEnder>();

        var harmony = new Harmony("net.heisenkebab.nightmaremode");
        harmony.PatchAll();

        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

    }

    private void registerConfig()
    {
        NightmareSettings.Enabled = Config.Bind("1General", "Enabled", true, "Enable this mod.");

        NightmareSettings.QuotaScaling = Config.Bind("2Quota", "Quota Scaling", 3f, new ConfigDescription("Adjust the quota multiplier", new AcceptableValueRange<float>(1f, 10f)));
        NightmareSettings.QuotaScaling.SettingChanged += (object sender, EventArgs e) => NightmareSettings.QuotaScaling.Value = Mathf.Round(NightmareSettings.QuotaScaling.Value * 10f) / 10f;

        NightmareSettings.ShouldSpawnThieves = Config.Bind("3Thieves", "Should Spawn Thieves", true, "Enable or disable thief spawns.");

        NightmareSettings.SpawnInterval = Config.Bind("3Thieves", "Spawn Interval", 20, new ConfigDescription("Adjust the spawn interval for thieves (seconds)", new AcceptableValueRange<int>(5, 60)));

        NightmareSettings.SpawnChance = Config.Bind("3Thieves", "Spawn Chance", 0.5f, new ConfigDescription("Adjust the spawn chance for thieves", new AcceptableValueRange<float>(0f, 1f)));
        NightmareSettings.SpawnChance.SettingChanged += (object sender, EventArgs e) =>
        NightmareSettings.SpawnChance.Value = Mathf.Round(NightmareSettings.SpawnChance.Value * 10f) / 10f;

        NightmareSettings.DamageTakenScaling = Config.Bind("4Damage", "Damage Taken Scaling", 3f, new ConfigDescription("Adjust the damage taken multiplier", new AcceptableValueRange<float>(1f, 10f)));
        NightmareSettings.DamageTakenScaling.SettingChanged += (object sender, EventArgs e) => NightmareSettings.DamageTakenScaling.Value = Mathf.Round(NightmareSettings.DamageTakenScaling.Value * 10f) / 10f;

        NightmareSettings.SpiderHealtScaling = Config.Bind("5Spider", "Spider Health Scaling", 4f, new ConfigDescription("Adjust the entities health multiplier", new AcceptableValueRange<float>(1f, 10f)));
        NightmareSettings.SpiderHealtScaling.SettingChanged += (object sender, EventArgs e) => NightmareSettings.SpiderHealtScaling.Value = Mathf.Round(NightmareSettings.SpiderHealtScaling.Value * 10f) / 10f;

        NightmareSettings.SpiderSpeedScaling = Config.Bind("5Spider", "Spider Speed Scaling", 2f, new ConfigDescription("Adjust the Spider Speed multiplier", new AcceptableValueRange<float>(1f, 10f)));
        NightmareSettings.SpiderSpeedScaling.SettingChanged += (object sender, EventArgs e) => NightmareSettings.SpiderSpeedScaling.Value = Mathf.Round(NightmareSettings.SpiderSpeedScaling.Value * 10f) / 10f;

        NightmareSettings.ShouldLimitCustomerPatience = Config.Bind("6Customers", "1Should Limit Customer Patience", true, "Force every customer to have a patience timer");

        NightmareSettings.CustomerPatienceSeconds = Config.Bind("6Customers", "2Customer Patience Seconds", 60f, new ConfigDescription("Adjust how many seconds customers wait before leaving", new AcceptableValueRange<float>(5f, 180f)));
        NightmareSettings.CustomerPatienceSeconds.SettingChanged += (object sender, EventArgs e) => NightmareSettings.CustomerPatienceSeconds.Value = Mathf.Round(NightmareSettings.CustomerPatienceSeconds.Value * 10f) / 10f;

        NightmareSettings.BoxContentAmount = Config.Bind("7Boxes", "Box Content", 5, new ConfigDescription("Adjust how many products a product box holds", new AcceptableValueRange<int>(1, 15)));

        NightmareSettings.VentPushOutSpeed = Config.Bind("8Vents", "Vent Push Out Speed", 1.15f, new ConfigDescription("Adjust how fast the vent pushes a player back out (game default 1.15)", new AcceptableValueRange<float>(0.1f, 10f)));
        NightmareSettings.VentPushOutSpeed.SettingChanged += (object sender, EventArgs e) => NightmareSettings.VentPushOutSpeed.Value = Mathf.Round(NightmareSettings.VentPushOutSpeed.Value * 100f) / 100f;

        NightmareSettings.DeathReset = Config.Bind("9GameReset", "1Death Reset Mode", DeathResetMode.AnyoneDies,
            "When a death wipes the run. Never = deaths never reset. AnyoneDies = a single death ends the run for everyone. EveryoneDies = only a full team wipe ends the run.");

        NightmareSettings.QuotaReset = Config.Bind("9GameReset", "2Quota Reset", true, "Wipe the run when the day ends without the quota being met.");

        ModSettingsRegistry.Register(
          PluginInfo.PLUGIN_GUID,
          new ModSettingsModOptions
          {
              Name = "NIGHTMARE Mode",
              Description = "A mod that makes the game a nightmare",
              Author = "Heisenkebab",
              Version = PluginInfo.PLUGIN_VERSION,
              NexusModsId = 6,
              ThunderstoreTeam = "Heisenkebab_Mods",
              ThunderstoreModName = "NIGHTMAREMode"
          });
    }
    class PluginInfo
    {
        public const string PLUGIN_GUID = "nightmaremode";
        public const string PLUGIN_NAME = "NIGHTMARE Mode";
        public const string PLUGIN_VERSION = "1.0.0";
    }
}
