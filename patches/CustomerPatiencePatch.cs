using HarmonyLib;
using NIGHTMAREMODE;
using UnityEngine.UI;

[HarmonyPatch(typeof(StoreBrowseBehaviour), "OnEnable")]
public static class CustomerPatiencePatch
{
    static void Postfix(StoreBrowseBehaviour __instance)
    {
        if (!NightmareSettings.Enabled.Value || !NightmareSettings.ShouldLimitCustomerPatience.Value)
        {
            return;
        }
        if (__instance.patienceCanvas == null)
        {
            return;
        }
        var patienceBar = __instance.patienceCanvas.GetComponentInChildren<Image>(true);
        if (patienceBar == null)
        {
            return;
        }
        __instance.patienceBar = patienceBar;
        __instance.hasPatience = true;
        __instance.maxPatience = NightmareSettings.CustomerPatienceSeconds.Value;
        __instance.curPatience = NightmareSettings.CustomerPatienceSeconds.Value;
        Plugin.Log.LogInfo($"Forced customer patience, max {NightmareSettings.CustomerPatienceSeconds.Value}s");
    }
}