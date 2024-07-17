namespace CognifyAntiCheat.Listener.Event.Impl;

public class IntroCutsceneEvent : CognifyAntiCheat.Listener.Event.Event
{
    public IntroCutsceneEvent(IntroCutscene introCutscene)
    {
        IntroCutscene = introCutscene;
    }

    public IntroCutscene IntroCutscene { get; }
}