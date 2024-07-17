namespace CognifyAntiCheat.Listener.Event.Impl;

public class TaskAddButtonEvent : CognifyAntiCheat.Listener.Event.Event
{
    public TaskAddButtonEvent(TaskAddButton taskAddButton)
    {
        TaskAddButton = taskAddButton;
    }

    public TaskAddButton TaskAddButton { get; }
}