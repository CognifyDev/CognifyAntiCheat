namespace CognifyAntiCheat.Listener.Event.Impl;

public class ControllerEvent : CognifyAntiCheat.Listener.Event.Event
{
    public ControllerEvent(ControllerManager manager)
    {
        Manager = manager;
    }

    public ControllerManager Manager { get; }
}