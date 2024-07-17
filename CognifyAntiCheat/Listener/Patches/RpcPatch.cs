using CognifyAntiCheat.Listener.Event.Impl.Player;
using HarmonyLib;
using Hazel;

namespace CognifyAntiCheat.Listener.Patches;

[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.HandleRpc))]
internal class RPCHandlerPatch
{
    [HarmonyPostfix]
    public static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] byte callId,
        [HarmonyArgument(1)] MessageReader reader)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new PlayerHandleRpcEvent(__instance, callId, reader), EventHandlerType.Postfix);
    }

    [HarmonyPrefix]
    public static bool Prefix(PlayerControl __instance, [HarmonyArgument(0)] byte callId,
        [HarmonyArgument(1)] MessageReader reader)
    {
        var result = ListenerManager.GetManager().ExecuteHandlers(new PlayerHandleRpcEvent(__instance, callId, reader),
            EventHandlerType.Prefix);
        return result;
    }
}