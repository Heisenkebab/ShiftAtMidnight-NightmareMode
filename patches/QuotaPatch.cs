using HarmonyLib;
using NIGHTMAREMODE;

// The HUD-facing quota isn't read from StoreManager.quota at all — it's delivered
// straight to clients as an RPC parameter. Scale it here so day 1 (and every day
// after, since this RPC fires each time gameplay values are pushed) shows correctly.
[HarmonyPatch(typeof(SaveManager), "Rpc_ActuallyGameplayValuesForClients")]
public static class ScaleSaveManagerQuotaPatch
{
    static void Prefix(ref float quota)
    {
        quota *= 1.5f;
        Plugin.Log.LogInfo($"Scaled SaveManager gameplay quota to {quota}");
    }
}

// Same story for the end-of-day report screen: it reads the quota from this RPC's
// own parameter, not from StoreManager.quota.
[HarmonyPatch(typeof(EndOfDayReport), "Rpc_UpdateVariables")]
public static class ScaleEndOfDayReportQuotaPatch
{
    static void Prefix(ref float quota)
    {
        quota *= 1.5f;
        Plugin.Log.LogInfo($"Scaled end-of-day quota to {quota}");
    }
}
