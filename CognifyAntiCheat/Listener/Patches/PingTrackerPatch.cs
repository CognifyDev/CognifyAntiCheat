using CognifyAntiCheat.Listener.Event.Impl.Game;
using HarmonyLib;

namespace CognifyAntiCheat.Listener.Patches;

[HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
public static class PingTrackerPatch
{
    private static void Postfix(PingTracker __instance)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new PingTrackerUpdateEvent(__instance), EventHandlerType.Postfix);
    }
}
