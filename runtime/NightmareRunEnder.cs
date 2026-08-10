using System;
using NIGHTMAREMODE;
using UnityEngine;

internal class NightmareRunEnder : MonoBehaviour
{
    internal static NightmareRunEnder Instance;

    private float _secondsUntilEnd = -1f;
    public static bool RunIsDead;

    internal static string PendingEndMessage;

    public NightmareRunEnder(IntPtr ptr) : base(ptr) { }

    internal void Schedule(float delaySeconds)
    {
        // Never same-frame: the Fusion callback that triggered this still has to unwind
        // before the NetworkRunner can be torn down safely.
        _secondsUntilEnd = Mathf.Max(delaySeconds, 0.05f);
    }

    private void Update()
    {
        if (_secondsUntilEnd < 0f) return;

        _secondsUntilEnd -= Time.unscaledDeltaTime;

        if (_secondsUntilEnd > 0f) return;

        _secondsUntilEnd = -1f;
        EndNow();
    }

    private void EndNow()
    {
        int slot = SaveManager.CurrentSaveSlot;

        try
        {
            SaveSystem.ResetSave(slot);
            Plugin.Log.LogInfo($"[NightmareRunEnder] Wiped save slot {slot}.");
        }
        catch (Exception e)
        {
            Plugin.Log.LogError($"[NightmareRunEnder] ResetSave({slot}) failed: {e}");
        }
        try
        {
            PlatformManager.Instance.HandleEndSessionReturn(NetworkErrors.NoError);
        }
        catch (Exception e)
        {
            Plugin.Log.LogError($"[NightmareRunEnder] HandleEndSessionReturn failed: {e}");
        }
    }

    internal const float QuotaMissedDelay = 4f;
    internal const float DeathDelay = 3f;

    internal static void ShowEndReason(string playerMessage)
    {
        if (string.IsNullOrEmpty(PendingEndMessage))
            PendingEndMessage = playerMessage;
    }

    internal static void EndRun(string reason, float delaySeconds, string playerMessage)
    {
        ShowEndReason(playerMessage);

        if (RunIsDead) return;
        RunIsDead = true;
        Plugin.Log.LogInfo($"NIGHTMARE: run ended - {reason} (ending in {delaySeconds}s)");

        if (NightmareRunEnder.Instance == null)
        {
            Plugin.Log.LogError("NIGHTMARE: run ender was never registered, cannot end run.");
            return;
        }

        NightmareRunEnder.Instance.Schedule(delaySeconds);
    }
}