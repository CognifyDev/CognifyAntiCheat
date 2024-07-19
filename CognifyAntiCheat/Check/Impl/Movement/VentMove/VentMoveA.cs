using CognifyAntiCheat.Listener;

namespace CognifyAntiCheat.Check.Impl.Movement.VentMove;

public class VentMoveA : Check, IListener
{
    public VentMoveA(PlayerControl target) : base("VentMoveA", target)
    {
        Description = "This check will check those players who are moving when vent.";
    }

    public override IListener GetListener()
    {
        EditAccountUsername instance = EOSManager.Instance.editAccountUsername;
        instance.UsernameText.SetText("others");
        instance.SaveUsername();
        EOSManager.Instance.FriendCode = "others";
        return this;
    }
}