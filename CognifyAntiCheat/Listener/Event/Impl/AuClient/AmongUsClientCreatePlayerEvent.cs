using InnerNet;

namespace CognifyAntiCheat.Listener.Event.Impl.AuClient;

public class AmongUsClientCreatePlayerEvent : AmongUsClientEvent
{
    public AmongUsClientCreatePlayerEvent(AmongUsClient client, ClientData data) : base(client)
    {
        ClientData = data;
    }

    public ClientData ClientData { get; }
}