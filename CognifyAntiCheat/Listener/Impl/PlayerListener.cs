using CognifyAntiCheat.Check;
using CognifyAntiCheat.Listener.Event.Impl.AuClient;

namespace CognifyAntiCheat.Listener.Impl;

public class PlayerListener : IListener
{
    public void OnPlayerJoin(AmongUsClientJoinLobbyEvent @event)
    {
        if (!AmongUsClient.Instance.AmHost) return;
        var player = @event.AmongUsClient.PlayerPrefab;
        CheckManager.GetManager(player).Register();
    }

    public void OnPlayerLeave(AmongUsClientLeaveEvent @event)
    {
        if (!AmongUsClient.Instance.AmHost) return;
        var player = @event.AmongUsClient.PlayerPrefab;
        CheckManager.GetManager(player).Clear();
    }
}