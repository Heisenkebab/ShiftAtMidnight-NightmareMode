using HarmonyLib;

namespace NIGHTMAREMODE.Patches;

[HarmonyPatch(typeof(RestockShelf), "RemoveAtStart")]
public static class TutorialShelfPatch
{
    static void Prefix(RestockShelf __instance)
    {
        if (!NightmareSettings.Enabled.Value) return;
        if (!__instance.tutorialShelf) return;
        if (!__instance.Object.HasStateAuthority) return;

        SaveManager saveManagerInstance = SaveManager.Instance;
        if (saveManagerInstance == null || saveManagerInstance.curDay != 1) return;

        SaveGameMode mode = (SaveGameMode)SaveManager.GetGameMode(SaveManager.CurrentSaveSlot);
        if (mode != SaveGameMode.Story) return;

        // The tutorial hands out exactly 2 boxes, so the most the shelf can be
        // stripped by is what those 2 boxes now hold between them.
        int itemAmount = NightmareSettings.BoxContentAmount.Value * 2;
        int before = __instance.removeAtStartAmount;
        __instance.removeAtStartAmount = itemAmount;
        Plugin.Log.LogInfo($"[Restock] Set items to {itemAmount} instead of {before}");
    }
}
