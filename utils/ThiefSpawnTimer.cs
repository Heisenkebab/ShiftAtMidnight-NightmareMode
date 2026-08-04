using System;
using NIGHTMAREMODE;
using UnityEngine;

// Not a Harmony patch: the base game only spawns thieves from its own scripted occurrences
// (CurrentDayManager.thieves / thievesShuffled), so there's no existing per-frame hook to
// piggyback a random check on. This adds its own timer on top of that, calling the game's
// own CurrentDayManager.SpawnThief() so thief selection/placement still uses vanilla logic.
internal class ThiefSpawnTimer : MonoBehaviour
{
    private float _timer;

    public ThiefSpawnTimer(IntPtr ptr) : base(ptr) { }

    private void Update()
    {
        if (!NightmareSettings.Enabled.Value || !NightmareSettings.ShouldSpawnThieves.Value) return;
        _timer += Time.deltaTime;

        if (_timer < NightmareSettings.SpawnInterval.Value) return;
        _timer = 0f;

        CurrentDayManager dayManager = CurrentDayManager.Instance;
        if (dayManager == null || !dayManager.startedDay) return;

        // Only the state authority (host) should trigger spawns, otherwise every
        // connected client would independently roll and spawn its own thief.
        if (!dayManager.HasStateAuthority) return;

        if (UnityEngine.Random.value < NightmareSettings.SpawnChance.Value)
        {
            Plugin.Log.LogInfo("[ThiefSpawnTimer] Roll succeeded, spawning thief.");
            dayManager.SpawnThief();
        }
    }
}
