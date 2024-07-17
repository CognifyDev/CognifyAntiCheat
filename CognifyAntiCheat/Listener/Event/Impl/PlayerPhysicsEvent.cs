namespace CognifyAntiCheat.Listener.Event.Impl;

public class PlayerPhysicsEvent : CognifyAntiCheat.Listener.Event.Event
{
    public PlayerPhysicsEvent(PlayerPhysics playerPhysics)
    {
        PlayerPhysics = playerPhysics;
    }

    public PlayerPhysics PlayerPhysics { get; }
}