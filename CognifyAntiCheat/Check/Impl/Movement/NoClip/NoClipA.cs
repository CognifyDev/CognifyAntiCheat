using CognifyAntiCheat.Listener;
using CognifyAntiCheat.Listener.Event.Impl.Player;
using CognifyAntiCheat.Utils;

namespace CognifyAntiCheat.Check.Impl.Movement.NoClip;

/// <summary>
/// This is useless to check any no-clip players
/// </summary>
public class NoClipA : Check, IListener
{
    public NoClipA(PlayerControl target) : base("NoClipA", target)
    {
        Description = "to check those players who use NoClip";
    }

    [EventHandler(EventHandlerType.Postfix)]
    public void OnPlayerFixedUpdate(PlayerFixedUpdateEvent @event)
    {
        if (!AmongUsClient.Instance.AmHost) return;
        var player = @event.Player;
        if (player.IsSamePlayer(PlayerControl.LocalPlayer)) return;
        if (!player.Collider.enabled)
        {
            Fail();
        }
    }

    public override IListener GetListener()
    {
        return this;
    }
}