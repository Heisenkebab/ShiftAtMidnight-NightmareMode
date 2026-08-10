using HarmonyLib;

namespace NIGHTMAREMODE.Patches;

[HarmonyPatch(typeof(SaveManager), "Rpc_ActuallyGameplayValuesForClients")]
public static class ScaleSaveManagerQuotaPatch
{
    static void Prefix(ref float quota, SaveManager __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;

        if (!__instance.HasStateAuthority) return;

        quota *= NightmareSettings.QuotaScaling.Value;
        Plugin.Log.LogInfo($"Scaled SaveManager gameplay quota to {quota}");
    }
}

[HarmonyPatch(typeof(EndOfDayReport), "Rpc_UpdateVariables")]
public static class ScaleEndOfDayReportQuotaPatch
{
    static void Prefix(ref float quota, EndOfDayReport __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;

        StoreManager storeManager = StoreManager.Instance;
        if (!storeManager) return;

        quota = storeManager.quota;

        // ShowQuota compares against eodValues.mandatoryRevenue, not this quota param, which is only displayed.
        __instance.eodValues.mandatoryRevenue = storeManager.quota;
        Plugin.Log.LogInfo($"Scaled end-of-day quota to {quota}");
    }
}
