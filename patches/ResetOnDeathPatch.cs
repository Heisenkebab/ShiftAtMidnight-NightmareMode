using HarmonyLib;
using NIGHTMAREMODE;

[HarmonyPatch(typeof(PlayerManager), "Rpc_Die")]
public static class ResetOnDeathPatch
{
    static void Postfix(PlayerManager __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;

        DeathResetMode mode = NightmareSettings.DeathReset.Value;
        if (mode == DeathResetMode.Never) return;

        string reason;
        string message;

        if (mode == DeathResetMode.AnyoneDies)
        {
            reason = "a player died";
            message = "A player died. The save file has been wiped.";
        }
        else
        {
            if (!EveryoneIsDead(__instance)) return;

            reason = "everyone died";
            message = "Your whole team died. The save file has been wiped.";
        }

        if (__instance.HasStateAuthority)
            NightmareRunEnder.EndRun(reason, NightmareRunEnder.DeathDelay, message);
        else
            NightmareRunEnder.ShowEndReason(message);
    }

    private static bool EveryoneIsDead(PlayerManager justDied)
    {
        StoreManager store = StoreManager.Instance;
        if (store == null || store.playerMans == null) return false;

        for (int i = 0; i < store.playerMans.Count; i++)
        {
            PlayerManager pm = store.playerMans[i];
            if (pm == null) continue;

            // The player that triggered this is dead whether or not the flag has landed yet.
            if (pm.Pointer == justDied.Pointer) continue;

            if (!pm.dead) return false;
        }

        return true;
    }
}