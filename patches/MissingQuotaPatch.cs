using HarmonyLib;
using NIGHTMAREMODE;

[HarmonyPatch(typeof(EndOfDayReport), "ShowQuota")]
public static class MissingQuotaPatch
{
    static void Postfix(EndOfDayReport __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;

        if (!NightmareSettings.QuotaReset.Value) return;

        if (__instance.alreadyCompleted) return;

        if (__instance.hitQuota) return;

        const string message = "You missed the quota. The save file has been wiped.";

        if (__instance.HasStateAuthority)
            NightmareRunEnder.EndRun("quota missed", NightmareRunEnder.QuotaMissedDelay, message);
        else
            NightmareRunEnder.ShowEndReason(message);
    }
}

// Stops the game writing a fresh save after ResetSave has wiped the slot.
[HarmonyPatch(typeof(SaveSystem), "SaveState")]
public static class BlockSaveOnDeadRun
{
    static bool Prefix() => !NightmareRunEnder.RunIsDead;
}
