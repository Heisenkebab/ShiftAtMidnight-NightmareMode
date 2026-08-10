using HarmonyLib;

namespace NIGHTMAREMODE.Patches;

[HarmonyPatch(typeof(Spider), "Start")]
public static class SpiderSpeedPatch
{
    static void Prefix(Spider __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;
        __instance.normalSpeed *= NightmareSettings.SpiderSpeedScaling.Value;
        __instance.runSpeed *= NightmareSettings.SpiderSpeedScaling.Value;
        __instance.annoyedSpeed *= NightmareSettings.SpiderSpeedScaling.Value;
        __instance.rampageSpeed *= NightmareSettings.SpiderSpeedScaling.Value;
        Plugin.Log.LogInfo($"Scaled speed to {NightmareSettings.SpiderSpeedScaling.Value}x");
    }
}