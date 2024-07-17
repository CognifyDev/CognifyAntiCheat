namespace CognifyAntiCheat.Listener.Event.Impl;

public class TaskPanelBehaviourEvent : CognifyAntiCheat.Listener.Event.Event
{
    public TaskPanelBehaviourEvent(TaskPanelBehaviour behaviour)
    {
        Behaviour = behaviour;
    }

    public TaskPanelBehaviour Behaviour { get; }
}