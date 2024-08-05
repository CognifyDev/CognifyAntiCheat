using CognifyAntiCheat.Listener;
using CognifyAntiCheat.Listener.Event.Impl.Player;
using CognifyAntiCheat.States;
using CognifyAntiCheat.Utils;

namespace CognifyAntiCheat.Check.Impl.Action.IllegalReport;

public class IllegalReportA : Check, IListener
{
    public IllegalReportA(PlayerControl target) : base("IllegalReportA", target)
    {
    }
    
    [EventHandler(EventHandlerType.Prefix)]
    public bool OnPlayerReport(PlayerReportDeadBodyEvent @event)
    {
        if (!AmongUsClient.Instance.AmHost) return true;
        var player = @event.Player;
        if (!player.IsSamePlayer(Target)) return true;
        if (GameStates.IsMeeting)
        {
            Fail();
            return !Cancelled;
        }

        return true;
    }

    public override IListener GetListener()
    {
        return this;
    }
}