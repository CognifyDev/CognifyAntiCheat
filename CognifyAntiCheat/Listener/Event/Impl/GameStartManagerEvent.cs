namespace CognifyAntiCheat.Listener.Event.Impl;

public class GameStartManagerEvent : CognifyAntiCheat.Listener.Event.Event
{
    public GameStartManagerEvent(GameStartManager manager)
    {
        GameStartManager = manager;
    }

    public GameStartManager GameStartManager { get; }
}