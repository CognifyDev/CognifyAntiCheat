namespace CognifyAntiCheat.Listener.Event;

/// <summary>
///     事件类
/// </summary>
public class Event
{
    protected Event()
    {
        Name = GetType().Name;
        Id = Name.GetHashCode();
    }

    public string Name { get; }
    public int Id { get; }
}