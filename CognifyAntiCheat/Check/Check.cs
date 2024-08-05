using CognifyAntiCheat.Config.Impl;
using CognifyAntiCheat.Listener;
using CognifyAntiCheat.Utils;

namespace CognifyAntiCheat.Check;

public abstract class Check
{
    public Check(string name, PlayerControl target)
    {
        Name = name;
        Description = "No description.";
        MaxViolations = (int) SettingsConfig.Instance.YamlReader!.GetInt($"checks.{Name}.max-violations")!;
        Enabled = (bool)SettingsConfig.Instance.YamlReader!.GetBool($"checks.{Name}.enable")!;
        Target = target;
    }

    public string Name { get; }
    public string Description { get; protected set; }
    
    public int MaxViolations { get; }
    
    public bool Enabled { get; }
    
    public PlayerControl Target { get; }

    public abstract IListener GetListener();

    private int _violations;

    public bool Cancelled { get; } = SettingsConfig.Instance.Cancelled;

    protected void Fail()
    {
        _violations ++;
        Main.Logger.LogInfo($"Player {Target.Data.PlayerName} has failed to pass {Name} Violations/MaxViolations " +
                            $"=> {_violations}/{MaxViolations}");
        if (_violations >= MaxViolations) Punish();
    }

    protected void Punish()
    {
        var kick = SettingsConfig.Instance.Kick;
        var ban = SettingsConfig.Instance.Ban;

        if (kick)
        {
            AmongUsClient.Instance.KickPlayer(Target.GetClientID(), ban);
            Main.Logger.LogInfo($"Player {Target.Data.PlayerName} has been punished due to {Name} Violations/MaxViolations " +
                                $"=> {_violations}/{MaxViolations}, Kick => {kick}, Ban => {ban}");
        }
    }
}