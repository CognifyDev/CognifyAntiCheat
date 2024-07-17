using Hazel;

namespace CognifyAntiCheat.Listener.Event.Impl.Player;

/// <summary>
///     一个Rpc被捕捉的时候，这个事件会触发
///     动作的执行者即Rpc发送者
/// </summary>
public class PlayerHandleRpcEvent : PlayerEvent
{
    public PlayerHandleRpcEvent(PlayerControl player, byte callId, MessageReader reader) : base(player)
    {
        CallId = callId;
        Reader = MessageReader.Get(reader);
    }

    /// <summary>
    ///     Rpc的CallId
    /// </summary>
    public byte CallId { get; }

    /// <summary>
    ///     Rpc读取操作器
    /// </summary>
    public MessageReader Reader { get; }
}