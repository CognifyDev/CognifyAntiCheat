using CognifyAntiCheat.Check;
using CognifyAntiCheat.Listener.Event.Impl.AuClient;
using CognifyAntiCheat.Listener.Event.Impl.Player;
using CognifyAntiCheat.Utils;

namespace CognifyAntiCheat.Listener.Impl;

public class PlayerListener : IListener
{
    [EventHandler(EventHandlerType.Postfix)]
    public void OnPlayerJoin(PlayerControlAwakeEvent @event)
    {
        if (!AmongUsClient.Instance.AmHost)
        {
            CheckManager.ReloadManager();
            return;
        }
        if (@event.Player == null) return;
        if (PlayerControl.LocalPlayer.IsSamePlayer(@event.Player)) return;
        var player = @event.Player;
        CheckManager.GetManager(player).Register();
    }

    [EventHandler(EventHandlerType.Postfix)]
    public void OnPlayerLeave(AmongUsClientLeaveEvent @event)
    {
        if (!AmongUsClient.Instance.AmHost) return;
        var player = @event.ClientData.Character;
        CheckManager.GetManager(player).Clear();
    }
}