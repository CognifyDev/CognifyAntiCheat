using CognifyAntiCheat.Listener.Event.Impl.TPBehaviour;
using HarmonyLib;

namespace CognifyAntiCheat.Listener.Patches;

[HarmonyPatch(typeof(TaskPanelBehaviour), nameof(TaskPanelBehaviour.SetTaskText))]
public static class TaskPanelPatch
{
    public static bool Prefix(TaskPanelBehaviour __instance)
    {
        return ListenerManager.GetManager()
            .ExecuteHandlers(new TaskPanelBehaviourSetTaskTextEvent(__instance), EventHandlerType.Prefix);
    }

    public static void Postfix(TaskPanelBehaviour __instance)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new TaskPanelBehaviourSetTaskTextEvent(__instance), EventHandlerType.Postfix);
    }
}