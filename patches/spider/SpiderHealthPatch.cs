using HarmonyLib;
using NIGHTMAREMODE;

[HarmonyPatch(typeof(Spider), "Rpc_ChangeHittableHealth")]
public static class EnemyHealthPatch
{
    static void Prefix(ref float health, Spider __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;
        if (!__instance.Object.HasStateAuthority) return;
        float healthBefore = health;
        health *= NightmareSettings.SpiderHealthScaling.Value;
        Plugin.Log.LogInfo($"Scaled enemy health to {NightmareSettings.SpiderHealthScaling.Value}x ({healthBefore} to {health})");
    }
}