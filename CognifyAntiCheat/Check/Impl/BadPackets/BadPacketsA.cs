﻿using System.Linq;
using CognifyAntiCheat.Constant;
using CognifyAntiCheat.Listener;
using CognifyAntiCheat.Listener.Event.Impl.Player;
using CognifyAntiCheat.Utils;

namespace CognifyAntiCheat.Check.Impl.BadPackets;

public class BadPacketsA : Check, IListener
{
    public BadPacketsA(PlayerControl target) : base("BadPacketsA", target)
    {
        Description = "This check will check those players who use AUM & SickoMenu";
    }

    [EventHandler(EventHandlerType.Postfix)]
    public void OnRPCReceived(PlayerHandleRpcEvent @event)
    {
        if (!AmongUsClient.Instance.AmHost) return;
        var player = @event.Player;
        if (player.IsSamePlayer(Target) && CheckConstant.AmongUsMenuAndForksRpcs.Contains(@event.CallId)) Fail();
    }

    public override IListener GetListener() => this;
}