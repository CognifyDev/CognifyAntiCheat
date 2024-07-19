﻿using CognifyAntiCheat.Listener;
using CognifyAntiCheat.Listener.Event.Impl.Player;
using CognifyAntiCheat.Utils;

namespace CognifyAntiCheat.Check.Impl.Movement.VentMove;

public class VentMoveA : Check, IListener
{
    public VentMoveA(PlayerControl target) : base("VentMoveA", target)
    {
        Description = "This check will check those players who are moving when vent.";
    }

    [EventHandler(EventHandlerType.Postfix)]
    public void OnPlayerFixedUpdate(PlayerFixedUpdateEvent @event)
    {
        if (!AmongUsClient.Instance.AmHost) return;
        var player = @event.Player;
        if (player.IsSamePlayer(PlayerControl.LocalPlayer)) return;
        if (player is { inVent: true, moveable: true })
        {
            Fail();
        }
    }

    public override IListener GetListener()
    {
        return this;
    }
}