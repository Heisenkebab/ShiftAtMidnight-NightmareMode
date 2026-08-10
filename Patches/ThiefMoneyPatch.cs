using HarmonyLib;

namespace NIGHTMAREMODE.Patches;

[HarmonyPatch(typeof(StoreManager), "ThiefCaught")]
public static class ThiefMoneyPatch
{
    static bool Prefix(StoreManager __instance)
    {
        if (!NightmareSettings.Enabled.Value) return true;

        float moneyValue = NightmareSettings.ThiefCaughtPayout.Value;

        if (moneyValue <= 0f) return false;

        __instance.ChangeRevenue("Items Returned", moneyValue);
        return false;
    }
}
