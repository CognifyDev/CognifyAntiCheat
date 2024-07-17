namespace CognifyAntiCheat.Listener.Event.Impl.Player;

public class LocalPlayerChatEvent : PlayerEvent
{
    public LocalPlayerChatEvent(PlayerControl sender, string text) : base(sender)
    {
        Text = text;
    }

    public string Text { get; }
}