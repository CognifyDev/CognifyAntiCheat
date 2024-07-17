namespace CognifyAntiCheat.Listener.Event.Impl.Game;

public class GameCheckEndEvent : GameEvent<LogicGameFlowNormal>
{
    public GameCheckEndEvent(LogicGameFlowNormal obj) : base(obj)
    {
    }
}