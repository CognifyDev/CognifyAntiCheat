using CognifyAntiCheat.Listener.Event.Impl.Game;
using HarmonyLib;

namespace CognifyAntiCheat.Listener.Patches;

[HarmonyPatch(typeof(OptionsMenuBehaviour))]
internal class ModOptionsPatch
{
    [HarmonyPatch(nameof(OptionsMenuBehaviour.Start))]
    [HarmonyPostfix]
    public static void OptionMenuBehaviourStartPostfix(OptionsMenuBehaviour __instance)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new OptionsMenuBehaviourStartEvent(__instance), EventHandlerType.Postfix);
    }
}
