using HarmonyLib;
using NIGHTMAREMODE;

[HarmonyPatch(typeof(PlayerManager), "TakeDamage")]
public static class PlayerDamagePatch
{
    static void Prefix(ref float damage, ref bool significantAnim, ref string type, PlayerManager __instance)
    {
        if (!NightmareSettings.Enabled.Value)
        {
            return;
        }
        damage *= NightmareSettings.DamageTakenScaling.Value;
    }
}
