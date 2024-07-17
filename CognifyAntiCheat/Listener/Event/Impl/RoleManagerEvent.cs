namespace CognifyAntiCheat.Listener.Event.Impl;

public class RoleManagerEvent : CognifyAntiCheat.Listener.Event.Event
{
    public RoleManagerEvent(RoleManager manager)
    {
        RoleManager = manager;
    }

    public RoleManager RoleManager { get; }
}