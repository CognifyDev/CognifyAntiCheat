namespace CognifyAntiCheat.Config.Impl;

public sealed class SettingsConfig : Config
{
    public static SettingsConfig Instance { get; private set; } = null!;

    public SettingsConfig() : base("Settings", "settings.yml")
    {
        start:
        Instance = this;
        ArrangeSettings();
        try
        {
            ArrangeConfigs();
        }
        catch
        {
            LoadConfig(true);
            goto start;
        }
    }

    private void ArrangeConfigs()
    {
        Kick = (bool) YamlReader!.GetBool("check-settings.kick")!;
        Ban = (bool) YamlReader!.GetBool("check-settings.ban")!;
        AddToBlacklist = (bool) YamlReader!.GetBool("check-settings.add-to-blacklist")!;
        Cancelled = (bool) YamlReader!.GetBool("check-settings.cancelled")!;
    }

    public bool Kick { get; private set; }
    public bool Ban { get; private set; }
    public bool AddToBlacklist { get; private set; }
    public bool Cancelled { get; private set; }
}