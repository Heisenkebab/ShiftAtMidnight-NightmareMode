using System;
using HarmonyLib;

namespace NIGHTMAREMODE.Patches;

[HarmonyPatch(typeof(PlayerManager), "Rpc_TakeDamage")]
public static class PlayerDamagePatch
{
    static void Prefix(ref float damage, PlayerManager __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;
        if (__instance.Object == null || !__instance.Object.HasStateAuthority) return;

        float damageBefore = damage;
        float damageScale = NightmareSettings.DamageTakenScaling.Value;

        HuntManager huntManager = HuntManager.Instance;
        if (huntManager != null && huntManager.huntInProgress)
            damageScale += 0.25f * Math.Max(CompleteTransactionPatch.amountOfDoppelgangerLetThrough - 1, 0);

        damage *= damageScale;

        Plugin.Log.LogInfo($"[Rpc_TakeDamage] Damage Scaled by {damageScale}x ({damageBefore} to {damage})");
    }
}
