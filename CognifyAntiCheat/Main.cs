using BepInEx;
using BepInEx.Unity.IL2CPP;
using CognifyAntiCheat.Utils;
using HarmonyLib;

namespace CognifyAntiCheat;

[BepInAutoPlugin(PluginGuid, PluginName)]
[BepInProcess("Among Us.exe")]
public partial class Main : BasePlugin
{
    public const string PluginName = "Cognify Anti-Cheat";
    public const string PluginGuid = "top.cognifydev.anticheat";
    public const string DisplayName = "CognifyAntiCheat";

    public static StackTraceLogger Logger { get; private set; }
    private Harmony Harmony { get; } = new(PluginGuid);

    public static Main Instance { get; private set; } = null!;

    public override void Load()
    {
        Instance = this;
        Logger = new StackTraceLogger($"   {DisplayName}");
        
        ResourceUtils.WriteToFileFromResource(
            "BepInEx/core/YamlDotNet.dll",
            "COG.Resources.InDLL.Depends.YamlDotNet.dll");
        ResourceUtils.WriteToFileFromResource(
            "BepInEx/core/YamlDotNet.xml",
            "COG.Resources.InDLL.Depends.YamlDotNet.xml");
        
        Harmony.PatchAll();
    }
}