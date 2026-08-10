using HarmonyLib;
using UnityEngine;

namespace NIGHTMAREMODE.Patches;

[HarmonyPatch(typeof(PlayerManager), "TakeDamage")]
public static class PlayerDamagePatch
{
    static void Prefix(ref float damage, ref bool significantAnim, ref string type, PlayerManager __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;

        float before = damage;
        damage *= NightmareSettings.DamageTakenScaling.Value;

        // TEMPORARY: works out whether TakeDamage runs on the victim's client, the host, or both.
        // Remove once the authority guard question is settled.
        var obj = __instance.Object;
        Plugin.Log.LogInfo(
            $"[Damage] frame={Time.frameCount} victim={(obj != null ? obj.Id.ToString() : "?")} " +
            $"victimInputAuth={(obj != null ? obj.InputAuthority.ToString() : "?")} " +
            $"iAmStateAuth={__instance.HasStateAuthority} iAmInputAuth={__instance.HasInputAuthority} " +
            $"type={type} {before} -> {damage} (x{NightmareSettings.DamageTakenScaling.Value})");
    }
}
