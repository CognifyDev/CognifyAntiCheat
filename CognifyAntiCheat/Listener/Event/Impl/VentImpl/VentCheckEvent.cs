namespace CognifyAntiCheat.Listener.Event.Impl.VentImpl;

public class VentCheckEvent : VentEvent
{
    private bool _canUse, _couldUse;
    private float _result;

    public VentCheckEvent(Vent vent, NetworkedPlayerInfo playerInfo, bool canUse, bool couldUse, float result) :
        base(vent)
    {
        PlayerInfo = playerInfo;
        _canUse = canUse;
        _couldUse = couldUse;
        _result = result;
    }

    public NetworkedPlayerInfo PlayerInfo { get; }

    public void SetCanUse(bool canUse)
    {
        _canUse = canUse;
    }

    public bool GetCanUse()
    {
        return _canUse;
    }

    public void SetCouldUse(bool couldUse)
    {
        _couldUse = couldUse;
    }

    public bool GetCouldUse()
    {
        return _couldUse;
    }

    public void SetResult(float result)
    {
        _result = result;
    }

    public float GetResult()
    {
        return _result;
    }
}