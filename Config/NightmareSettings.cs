using System;
using BepInEx.Configuration;
using UnityEngine;

namespace NIGHTMAREMODE;

internal static class NightmareSettings
{
    public static ConfigEntry<bool> Enabled;
    public static ConfigEntry<float> QuotaScaling;
    public static ConfigEntry<bool> ShouldSpawnThieves;
    public static ConfigEntry<int> SpawnInterval;
    public static ConfigEntry<int> SpawnChance;
    public static ConfigEntry<float> ThiefCaughtPayout;
    public static ConfigEntry<float> DamageTakenScaling;
    public static ConfigEntry<float> SpiderHealthScaling;
    public static ConfigEntry<float> SpiderSpeedScaling;
    public static ConfigEntry<bool> ShouldLimitCustomerPatience;
    public static ConfigEntry<int> CustomerPatienceSeconds;
    public static ConfigEntry<int> BoxContentAmount;
    public static ConfigEntry<float> VentPushOutSpeed;
    public static ConfigEntry<DeathResetMode> DeathReset;
    public static ConfigEntry<bool> QuotaReset;

    internal static void Bind(ConfigFile config)
    {
        Enabled = config.Bind("1General", "Enabled", true, "Enable this mod.");

        QuotaScaling = BindRounded(config, "2Quota", "Quota Scaling", 2f, 1f, 10f, "Adjust the quota multiplier.");

        ShouldSpawnThieves = config.Bind("3Thieves", "1Should Spawn Thieves", true, "Enable or disable thief spawns.");

        SpawnInterval = config.Bind("3Thieves", "2Spawn Interval", 20, new ConfigDescription("Adjust the spawn interval for thieves, in seconds.", new AcceptableValueRange<int>(5, 60)));

        SpawnChance = config.Bind("3Thieves", "3Spawn Chance", 50, new ConfigDescription("Adjust the spawn chance for thieves.", new AcceptableValueRange<int>(1, 100)));

        ThiefCaughtPayout = BindRounded(config, "3Thieves", "4Thief Caught Payout", 0, 0f, 10f, "Adjust how much money you get back when a thief is caught.");

        DamageTakenScaling = BindRounded(config, "4Damage", "Damage Taken Scaling", 3f, 1f, 10f, "Adjust the damage taken multiplier.");

        SpiderHealthScaling = BindRounded(config, "5Spider", "Spider Health Scaling", 1.2f, 1f, 10f, "Adjust the spider health multiplier.");

        SpiderSpeedScaling = BindRounded(config, "5Spider", "Spider Speed Scaling", 2f, 1f, 10f, "Adjust the spider speed multiplier.");

        ShouldLimitCustomerPatience = config.Bind("6Customers", "1Should Limit Customer Patience", true, "Force every customer to have a patience timer.");

        CustomerPatienceSeconds = config.Bind("6Customers", "2Customer Patience Seconds", 60, new ConfigDescription("Adjust how many seconds customers wait before leaving.", new AcceptableValueRange<int>(5, 180)));

        BoxContentAmount = config.Bind("7Boxes", "Box Content", 5, new ConfigDescription("Adjust how many products a product box holds.", new AcceptableValueRange<int>(1, 15)));

        VentPushOutSpeed = BindRounded(config, "8Vents", "Vent Push Out Speed", 2.30f, 0.1f, 10f, "Adjust how fast the vent pushes a player back out (game default 1.15).", 2);

        DeathReset = config.Bind("9GameReset", "1Death Reset Mode", DeathResetMode.AnyoneDies,
            "When a death wipes the run. Never = deaths never reset. AnyoneDies = a single death ends the run for everyone. EveryoneDies = only a full team wipe ends the run.");

        QuotaReset = config.Bind("9GameReset", "2Quota Reset", true, "Wipe the run when the day ends without the quota being met.");
    }
    private static ConfigEntry<float> BindRounded(ConfigFile config, string section, string key,
       float defaultValue, float min, float max, string description, int decimals = 1)
    {
        ConfigEntry<float> entry = config.Bind(section, key, defaultValue,
            new ConfigDescription(description, new AcceptableValueRange<float>(min, max)));

        float factor = Mathf.Pow(10f, decimals);
        entry.SettingChanged += (object sender, EventArgs e) => entry.Value = Mathf.Round(entry.Value * factor) / factor;
        return entry;
    }
}
