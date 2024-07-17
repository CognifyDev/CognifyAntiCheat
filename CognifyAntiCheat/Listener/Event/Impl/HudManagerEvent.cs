namespace CognifyAntiCheat.Listener.Event.Impl;

public class HudManagerEvent : CognifyAntiCheat.Listener.Event.Event
{
    public HudManagerEvent(HudManager manager)
    {
        Manager = manager;
    }

    public HudManager Manager { get; }
}