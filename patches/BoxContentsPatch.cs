using HarmonyLib;
using UnityEngine;

namespace NIGHTMAREMODE.Patches;

[HarmonyPatch(typeof(PickupObject), "Spawned")]
public static class BoxContentsPatch
{
    static void Postfix(PickupObject __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;
        if (__instance.Object == null || !__instance.Object.HasStateAuthority) return;

        // Only boxes carry a stock count, every other PickupObject leaves these at 0.
        if (__instance.itemStorage <= 0 && __instance.itemStorage2 <= 0) return;
        if (__instance.itemStorage != 15) return;

        int value = NightmareSettings.BoxContentAmount.Value;
        int before = __instance.itemStorage;
        int before2 = __instance.itemStorage2;

        __instance.ChangeAmountOfItems(value, 0);
        Plugin.Log.LogInfo($"[PickupObject] Change box contents to {value} on objectIndex {__instance.objectIndex} ({before}/{before2} to {__instance.itemStorage}/{__instance.itemStorage2})");
    }
}
