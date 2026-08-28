using System;
using HarmonyLib;

namespace NIGHTMAREMODE.Patches;

[HarmonyPatch(typeof(Spider), "Start")]
public static class SpiderHealthPatch
{
    static void Prefix(Spider __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;
        if (__instance.Object == null || !__instance.Object.HasStateAuthority) return;

        int playerCount = __instance.Object.Runner.SessionInfo.PlayerCount;

        float healthBefore = __instance.hittable.health;
        float health = NightmareSettings.SpiderHealthScaling.Value + (0.2f * Math.Max(CompleteTransactionPatch.amountOfDoppelgangerLetThrough - 1, 0) * playerCount);

        __instance.hittable.health *= health;
        __instance.hittable.maxHealth *= health;

        Plugin.Log.LogInfo($"[Spider] Scaled enemy health to {health}x ({healthBefore} to {__instance.hittable.health})");
    }
}
