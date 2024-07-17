using System.Collections.Generic;

namespace CognifyAntiCheat.Config;

/// <summary>
/// NOT FINISHED YET
/// </summary>
public class ConfigManager : IManager<Config>
{
    private static ConfigManager? _manager;

    public static ConfigManager GetManager() => _manager ??= new ConfigManager();

    private readonly List<Config> _configs = new();

    public string GetName() => "ConfigManager";

    public void Register(Config t)
    {
        _configs.Add(t);
    }

    public void ReloadManager()
    {
        _manager = new ConfigManager();
    }

    public Config[] Get() => _configs.ToArray();
}