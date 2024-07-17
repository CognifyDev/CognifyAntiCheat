namespace CognifyAntiCheat.Listener.Event.Impl;

public class GameEvent<T> : CognifyAntiCheat.Listener.Event.Event
{
    public GameEvent(T obj)
    {
        Object = obj;
    }

    public T Object { get; }
}