using HarmonyLib;

[HarmonyPatch(typeof(StoreManager), "set_Quota")]
public static class ScaleQuotaPatch
{
    static void Prefix(ref float value)
    {
        value *= 1.5f;
    }
}