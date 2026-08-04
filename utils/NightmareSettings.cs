using BepInEx.Configuration;

namespace NIGHTMAREMODE;

internal static class NightmareSettings
{
    public static ConfigEntry<bool> Enabled;
    public static ConfigEntry<float> QuotaScaling;
    public static ConfigEntry<bool> ShouldSpawnThieves;
    public static ConfigEntry<int> SpawnInterval;
    public static ConfigEntry<float> SpawnChance;
    public static ConfigEntry<float> DamageTakenScaling;
    public static ConfigEntry<float> SpiderHealtScaling;
    public static ConfigEntry<float> SpiderSpeedScaling;
    public static ConfigEntry<bool> ShouldLimitCustomerPatience;
    public static ConfigEntry<float> CustomerPatienceSeconds;
    public static ConfigEntry<int> BoxContentAmount;
}
