using HarmonyLib;
using NIGHTMAREMODE;

[HarmonyPatch(typeof(SaveManager), "Rpc_ActuallyGameplayValuesForClients")]
public static class ScaleSaveManagerQuotaPatch
{
    static void Prefix(ref float quota)
    {
        if (!NightmareSettings.Enabled.Value)
        {
            return;
        }

        quota *= NightmareSettings.QuotaScaling.Value;
        Plugin.Log.LogInfo($"Scaled SaveManager gameplay quota to {quota}");
    }
}

[HarmonyPatch(typeof(EndOfDayReport), "Rpc_UpdateVariables")]
public static class ScaleEndOfDayReportQuotaPatch
{
    static void Prefix(ref float quota)
    {
        if (!NightmareSettings.Enabled.Value)
        {
            return;
        }

        quota *= NightmareSettings.QuotaScaling.Value;
        Plugin.Log.LogInfo($"Scaled end-of-day quota to {quota}");
    }
}
