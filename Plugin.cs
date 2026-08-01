using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace NIGHTMAREMODE;

[BepInPlugin("nightmaremode", "NIGHTMARE Mode", "1.0.0")]
public class Plugin : BasePlugin
{
    internal static new ManualLogSource Log;

    public override void Load()
    {
        // Plugin startup logic
        Log = base.Log;
        var harmony = new Harmony("net.heisenkebab.nightmaremode");
        harmony.PatchAll();
        Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
        
    }
}
