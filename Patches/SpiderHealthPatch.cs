using HarmonyLib;

namespace NIGHTMAREMODE.Patches;

[HarmonyPatch(typeof(Spider), "Start")]
public static class SpiderHealthPatch
{
    static void Prefix(Spider __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;
        if (!__instance.Object.HasStateAuthority) return;

        float healthBefore = __instance.hittable.health;
        __instance.hittable.health *= NightmareSettings.SpiderHealthScaling.Value;
        __instance.hittable.maxHealth *= NightmareSettings.SpiderHealthScaling.Value;
        Plugin.Log.LogInfo($"Scaled enemy health to {NightmareSettings.SpiderHealthScaling.Value}x ({healthBefore} to {__instance.hittable.health})");
    }
}
