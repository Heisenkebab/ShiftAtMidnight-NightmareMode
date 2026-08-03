using HarmonyLib;
using NIGHTMAREMODE;

[HarmonyPatch(typeof(PlayerManager), "TakeDamage")]
public static class PlayerDamagePatch
{
    static void Prefix(ref float damage, ref bool significantAnim, ref string type)
    {
        if (!NightmareSettings.Enabled.Value)
        {
            return;
        }
        damage *= NightmareSettings.DamageScaling.Value;
        Plugin.Log.LogInfo($"Scaled damage to {NightmareSettings.DamageScaling.Value}x");
    }
}
