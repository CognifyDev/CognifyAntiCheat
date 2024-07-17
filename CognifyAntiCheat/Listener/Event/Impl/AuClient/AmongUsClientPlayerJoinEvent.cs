using InnerNet;

namespace CognifyAntiCheat.Listener.Event.Impl.AuClient;

public class AmongUsClientPlayerJoinEvent : AmongUsClientEvent
{
    public ClientData ClientData;

    public AmongUsClientPlayerJoinEvent(AmongUsClient client, ClientData data) : base(client)
    {
        ClientData = data;
    }
}