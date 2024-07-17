namespace CognifyAntiCheat.Listener.Event.Impl;

public class AmongUsClientEvent : CognifyAntiCheat.Listener.Event.Event
{
    public AmongUsClientEvent(AmongUsClient client)
    {
        AmongUsClient = client;
    }

    public AmongUsClient AmongUsClient { get; }
}