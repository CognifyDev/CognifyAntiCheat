namespace CognifyAntiCheat.Listener.Event.Impl.Player;

public class PlayerExileBeginEvent : PlayerEvent
{
    public PlayerExileBeginEvent(PlayerControl? player, ExileController controller, NetworkedPlayerInfo? exiled,
        bool tie) : base(player!)
    {
        ExileController = controller;
        Exiled = exiled;
        Tie = tie;
    }

    /// <summary>
    ///     驱逐控制器
    /// </summary>
    public ExileController ExileController { get; }

    public NetworkedPlayerInfo? Exiled { get; }
    public bool Tie { get; }
}