using HarmonyLib;
using NIGHTMAREMODE;
using UnityEngine;

[HarmonyPatch(typeof(VentTrigger), "Rpc_EnterExitVent")]
public static class VentPushOutSpeedPatch
{
    private static readonly int MySpeed = Animator.StringToHash("MySpeed");

    static void Postfix(VentTrigger __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;

        if (__instance.ventPushOutAnim == null || __instance.playersInVent == null) return;

        if (__instance.playersInside < 1) return;

        __instance.ventPushOutAnim.SetFloat(MySpeed, NightmareSettings.VentPushOutSpeed.Value);
    }
}
