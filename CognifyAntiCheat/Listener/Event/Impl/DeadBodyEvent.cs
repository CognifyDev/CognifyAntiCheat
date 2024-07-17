namespace CognifyAntiCheat.Listener.Event.Impl;

public class DeadBodyEvent : CognifyAntiCheat.Listener.Event.Event
{
    public DeadBodyEvent(DeadBody deadBody)
    {
        DeadBody = deadBody;
    }

    public DeadBody DeadBody { get; }
}