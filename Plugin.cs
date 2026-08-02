using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;

namespace NIGHTMAREMODE;

[BepInPlugin(PluginInfo.PLUGIN_GUID,PluginInfo.PLUGIN_NAME,PluginInfo.PLUGIN_VERSION)]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;

    public override void Load()
    {
        // Plugin startup logic
        Log = base.Log;
        var harmony = new Harmony("net.heisenkebab.nightmaremode");
        harmony.PatchAll();

        //Register ThiefSpawnTimer
        ClassInjector.RegisterTypeInIl2Cpp<ThiefSpawnTimer>();
        var thiefTimerObj = new GameObject("NightmareModeThiefTimer");
        GameObject.DontDestroyOnLoad(thiefTimerObj);
        thiefTimerObj.AddComponent<ThiefSpawnTimer>();

        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");

    }
}
