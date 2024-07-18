using System.Linq;
using CognifyAntiCheat.Config.Impl;
using CognifyAntiCheat.Listener;
using CognifyAntiCheat.Listener.Event.Impl.Player;
using CognifyAntiCheat.Utils;

namespace CognifyAntiCheat.Check;

public abstract class Check : IListener
{
    public Check(string name, PlayerControl target)
    {
        Name = name;
        MaxViolations = (int) SettingsConfig.Instance.YamlReader!.GetInt($"checks.{Name}.max-violations")!;
        Enabled = (bool)SettingsConfig.Instance.YamlReader!.GetBool($"checks.{Name}.enable")!;
        Target = target;
    }

    public string Name { get; }
    public string? Description { get; protected set; }
    
    public int MaxViolations { get; }
    
    public bool Enabled { get; }
    
    public PlayerControl Target { get; }

    public abstract IListener GetListener();

    private int _violations;
    
    [EventHandler(EventHandlerType.Postfix)]
    public void OnPlayerUpdate(PlayerFixedUpdateEvent @event)
    {
        foreach (var playerControl in PlayerUtils.GetAllPlayers())
        {
            if (Target.IsSamePlayer(playerControl) && playerControl.Data.Disconnected)
            {
                ListenerManager.GetManager().UnRegisterHandlers(ListenerManager.GetManager().GetHandlers(this).ToArray());
            }
        }
    }

    protected void Fail()
    {
        Main.Logger.LogInfo($"Player {Target.Data.PlayerName} has failed to pass {Name} Violations/MaxViolations " +
                            $"=> {_violations}/{MaxViolations}");
        _violations ++;
        if (_violations >= MaxViolations) Punish();
    }

    protected void Punish()
    {
        var kick = SettingsConfig.Instance.Kick;
        var ban = SettingsConfig.Instance.Ban;

        if (kick)
        {
            AmongUsClient.Instance.KickPlayer(Target.PlayerId, ban);
            Main.Logger.LogInfo($"Player {Target.Data.PlayerName} has been punished due to {Name} Violations/MaxViolations " +
                                $"=> {_violations}/{MaxViolations}, Kick => {kick}, Ban => {ban}");
        }
    }
}