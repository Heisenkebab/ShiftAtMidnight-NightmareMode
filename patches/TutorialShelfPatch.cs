using HarmonyLib;
using NIGHTMAREMODE;

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

        //Because we get 2 boxes we can only remove the maxAmount from a box * 2
        int itemAmount = NightmareSettings.BoxContentAmount.Value * 2;
        __instance.removeAtStartAmount = itemAmount;
        Plugin.Log.LogInfo($"[Restock] Set items to {itemAmount} instead of 20");
    }
}
