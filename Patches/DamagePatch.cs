using System;
using HarmonyLib;

namespace NIGHTMAREMODE.Patches;

[HarmonyPatch(typeof(PlayerManager), "TakeDamage")]
public static class PlayerDamagePatch
{
    static void Prefix(ref float damage, ref bool significantAnim, ref string type, PlayerManager __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;

        float damageBefore = damage;
        HuntManager huntManager = HuntManager.Instance;
        if (huntManager != null && huntManager.huntInProgress)
        {
            float damageScale = NightmareSettings.DamageTakenScaling.Value + (0.25f * Math.Max(CompleteTransactionPatch.amountOfDoppelgangerLetThrough - 1, 0));
            damage *= damageScale;
            Plugin.Log.LogInfo($"[TakeDamage] Damage Scaled by {damageScale}x ({damageBefore} to {damage})");
            return;
        }

        damage *= NightmareSettings.DamageTakenScaling.Value;

        Plugin.Log.LogInfo($"[TakeDamage] Damage Scaled by {NightmareSettings.DamageTakenScaling.Value}x ({damageBefore} to {damage})");
    }
}
