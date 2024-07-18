using BepInEx;
using BepInEx.Unity.IL2CPP;
using CognifyAntiCheat.Check;
using CognifyAntiCheat.Check.Impl.BadPackets;
using CognifyAntiCheat.Config;
using CognifyAntiCheat.Config.Impl;
using CognifyAntiCheat.Listener;
using CognifyAntiCheat.Listener.Impl;
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

    public static StackTraceLogger Logger { get; private set; } = null!;
    private Harmony Harmony { get; } = new(PluginGuid);

    public static Main Instance { get; private set; } = null!;

    public override void Load()
    {
        Instance = this;
        Logger = new StackTraceLogger($"   {DisplayName}");
        
        ResourceUtils.WriteToFileFromResource(
            "BepInEx/core/YamlDotNet.dll",
            "CognifyAntiCheat.Resources.InDLL.Depends.YamlDotNet.dll");
        ResourceUtils.WriteToFileFromResource(
            "BepInEx/core/YamlDotNet.xml",
            "CognifyAntiCheat.Resources.InDLL.Depends.YamlDotNet.xml");
        
        // register configs
        ConfigManager.GetManager().Register(new SettingsConfig());
        
        // register listeners
        ListenerManager.GetManager().RegisterListeners(new IListener[]
        {
            new PlayerListener()
        });
        
        // register checks
        CheckManager.RegisterChecks(new[]
        {
            typeof(BadPacketsA)
        });
        
        Harmony.PatchAll();
    }
}