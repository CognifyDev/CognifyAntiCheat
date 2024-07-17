using CognifyAntiCheat.Listener.Event.Impl.DBody;
using HarmonyLib;

namespace CognifyAntiCheat.Listener.Patches;

[HarmonyPatch(typeof(DeadBody), nameof(DeadBody.OnClick))]
internal class DeadBodyClickPatch
{
    [HarmonyPrefix]
    public static bool Prefix(DeadBody __instance)
    {
        return ListenerManager.GetManager()
            .ExecuteHandlers(new DeadBodyClickEvent(__instance), EventHandlerType.Prefix);
    }

    [HarmonyPostfix]
    public static void Postfix(DeadBody __instance)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new DeadBodyClickEvent(__instance), EventHandlerType.Postfix);
    }
}