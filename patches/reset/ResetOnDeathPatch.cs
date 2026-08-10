using Fusion;
using HarmonyLib;

namespace NIGHTMAREMODE.Patches;

[HarmonyPatch(typeof(PlayerManager), "Rpc_Die")]
public static class ResetOnDeathPatch
{
    static void Postfix(PlayerManager __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;

        DeathResetMode mode = NightmareSettings.DeathReset.Value;

        string message = "Your whole team died. The save file has been wiped.";
        string singleplayerMessage = "You died. The save file has been wiped.";

        // Rpc_Die is the team wipe, so gate it on state authority to end the run once for the lobby.
        // Solo is its own case: the one death is already the wipe, so anything but Never ends the run.
        var net = FusionNetworkManager.Instance;

        if (net == null)
        {
            Plugin.Log.LogError("[ResetOnDeath] Instance of FusionNetworkManager does not exist");
            return;
        }
        bool isSolo = net.IsSoloMode();
        if ((__instance.HasStateAuthority && mode == DeathResetMode.EveryoneDies) || (isSolo && mode != DeathResetMode.Never))
            NightmareRunEnder.EndRun("everyone died", NightmareRunEnder.DeathDelay, isSolo ? singleplayerMessage : message);
        else
            NightmareRunEnder.ShowEndReason(message);
    }
}