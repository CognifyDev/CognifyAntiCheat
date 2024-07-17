namespace CognifyAntiCheat.Listener.Event.Impl;

public class TaskAdderGameEvent : CognifyAntiCheat.Listener.Event.Event
{
    public TaskAdderGameEvent(TaskAdderGame taskAdderGame)
    {
        TaskAdderGame = taskAdderGame;
    }

    public TaskAdderGame TaskAdderGame { get; }
}