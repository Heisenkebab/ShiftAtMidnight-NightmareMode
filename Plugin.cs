using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using ModSettingsMenu.Api;
using UnityEngine;
using ModInfo = NIGHTMAREMODE.MyPluginInfo;

namespace NIGHTMAREMODE;

[BepInPlugin(ModInfo.PLUGIN_GUID, ModInfo.PLUGIN_NAME, ModInfo.PLUGIN_VERSION)]
[BepInDependency(ModSettingsMenu.PluginInfo.PLUGIN_GUID)]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;

    public override void Load()
    {
        // Plugin startup logic
        Log = base.Log;

        NightmareSettings.Bind(Config);
        RegisterModSettingsMenu();
        RegisterRuntimeObjects();

        var harmony = new Harmony("net.heisenkebab.nightmaremode");
        harmony.PatchAll();

        Log.LogInfo($"Plugin {ModInfo.PLUGIN_NAME} {ModInfo.PLUGIN_VERSION} is loaded!");

    }
    private static void RegisterModSettingsMenu()
    {
        ModSettingsRegistry.Register(
         ModInfo.PLUGIN_GUID,
         new ModSettingsModOptions
         {
             Name = ModInfo.PLUGIN_NAME,
             Description = "A mod that makes the game a nightmare",
             Author = "Heisenkebab",
             Version = ModInfo.PLUGIN_VERSION,
             NexusModsId = 6,
             ThunderstoreTeam = "Heisenkebab_Mods",
             ThunderstoreModName = "NIGHTMARE_Mode"
         });
    }
    private void RegisterRuntimeObjects()
    {
        //Register ThiefSpawnTimer
        ClassInjector.RegisterTypeInIl2Cpp<ThiefSpawnTimer>();
        GameObject thiefTimerObj = new GameObject("NightmareModeThiefTimer");
        GameObject.DontDestroyOnLoad(thiefTimerObj);
        thiefTimerObj.AddComponent<ThiefSpawnTimer>();

        //Register NightmareRunEnder. Must survive the scene unload it triggers.
        ClassInjector.RegisterTypeInIl2Cpp<NightmareRunEnder>();
        GameObject runEnderObj = new GameObject("NightmareModeRunEnder");
        GameObject.DontDestroyOnLoad(runEnderObj);
        NightmareRunEnder.Instance = runEnderObj.AddComponent<NightmareRunEnder>();
    }

}
