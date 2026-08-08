using HarmonyLib;
using NIGHTMAREMODE;

[HarmonyPatch(typeof(PlayerManager), "Rpc_Downed")]
public static class ResetOnDownPatch
{
    static void Postfix(PlayerManager __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;

        DeathResetMode mode = NightmareSettings.DeathReset.Value;

        string message = "A player died. The save file has been wiped.";
        if (__instance.HasStateAuthority && mode == DeathResetMode.AnyoneDies)
            NightmareRunEnder.EndRun("a player died", NightmareRunEnder.DeathDelay, message);
        else
            NightmareRunEnder.ShowEndReason(message);
    }
}