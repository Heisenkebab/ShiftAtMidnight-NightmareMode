using HarmonyLib;

namespace NIGHTMAREMODE.Patches;

[HarmonyPatch(typeof(MainMenu), "Start")]
public static class RunEndedMessagePatch
{
    static void Postfix(MainMenu __instance)
    {
        NightmareRunEnder.RunIsDead = false;

        RunEndedPanelPatch.ActiveMessage = NightmareRunEnder.PendingEndMessage;
        NightmareRunEnder.PendingEndMessage = null;

        if (string.IsNullOrEmpty(RunEndedPanelPatch.ActiveMessage)) return;

        __instance.ShowDisconnectedMessage(true, NetworkErrors.Disconnected);
    }
}

[HarmonyPatch(typeof(MainMenu), "ShowDisconnectedMessage")]
public static class RunEndedPanelPatch
{
    internal static string ActiveMessage;

    static void Postfix(MainMenu __instance, bool show, NetworkErrors _errorType)
    {
        if (!show || string.IsNullOrEmpty(ActiveMessage)) return;

        if (__instance.networkErrorObjTitleText != null)
            __instance.networkErrorObjTitleText.text = "NIGHTMARE MODE";

        if (__instance.networkErrorObjDescText != null)
            __instance.networkErrorObjDescText.text = ActiveMessage;
    }
}