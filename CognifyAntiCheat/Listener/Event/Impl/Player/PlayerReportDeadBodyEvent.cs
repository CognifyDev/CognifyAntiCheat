namespace CognifyAntiCheat.Listener.Event.Impl.Player;

/// <summary>
///     当一个玩家启动会议(包括报告实体、紧急会议)的时候，这个事件会被触发
/// </summary>
public class PlayerReportDeadBodyEvent : PlayerEvent
{
    public PlayerReportDeadBodyEvent(PlayerControl player, NetworkedPlayerInfo? target) : base(player)
    {
        Target = target;
    }

    /// <summary>
    ///     如果为报告实体而启动的会议，那么这一项会是被报告实体的PlayerInfo
    ///     如果是紧急会议，那么这个会为null
    /// </summary>
    public NetworkedPlayerInfo? Target { get; }
}