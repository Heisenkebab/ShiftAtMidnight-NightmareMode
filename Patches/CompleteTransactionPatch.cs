using HarmonyLib;
using UnityEngine.UI;

namespace NIGHTMAREMODE.Patches;

[HarmonyPatch(typeof(TransactionManager), "CompleteTransaction")]
public static class CompleteTransactionPatch
{
    public static int amountOfDoppelgangerLetThrough { get; internal set; } = 0;
    static void Postfix(TransactionManager __instance)
    {
        StoreBrowseBehaviour storeBrowseBehaviour = __instance.curNpcScript;
        if (!storeBrowseBehaviour.isDoppelganger) return;
        amountOfDoppelgangerLetThrough++;
        Plugin.Log.LogInfo($"[CompleteTransaction] Let doppelganger through (Total: {amountOfDoppelgangerLetThrough})");
    }
}
