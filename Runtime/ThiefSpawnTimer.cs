using System;
using UnityEngine;

namespace NIGHTMAREMODE;

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

        if (!dayManager.HasStateAuthority) return;

        if (UnityEngine.Random.value < (NightmareSettings.SpawnChance.Value / 100f))
        {
            Plugin.Log.LogInfo("[ThiefSpawnTimer] Roll succeeded, spawning thief.");
            dayManager.SpawnThief();
        }
    }
}
