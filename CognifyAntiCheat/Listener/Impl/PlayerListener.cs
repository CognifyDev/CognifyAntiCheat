using System.Threading;
using CognifyAntiCheat.Check;
using CognifyAntiCheat.Listener.Event.Impl.AuClient;

namespace CognifyAntiCheat.Listener.Impl;

public class PlayerListener : IListener
{
    [EventHandler(EventHandlerType.Postfix)]
    public void OnPlayerJoin(AmongUsClientPlayerJoinEvent @event)
    {
        if (!AmongUsClient.Instance.AmHost) return;
        new Thread(() =>
        {
            Thread.Sleep(500);
            var player = @event.ClientData.Character;
            CheckManager.GetManager(player).Register();
        }).Start();
    }

    [EventHandler(EventHandlerType.Postfix)]
    public void OnPlayerLeave(AmongUsClientLeaveEvent @event)
    {
        if (!AmongUsClient.Instance.AmHost) return;
        var player = @event.ClientData.Character;
        CheckManager.GetManager(player).Clear();
    }
}