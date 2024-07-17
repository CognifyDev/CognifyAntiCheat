using CognifyAntiCheat.Listener.Event.Impl.AuClient;
using CognifyAntiCheat.Listener.Event.Impl.Controller;
using CognifyAntiCheat.Listener.Event.Impl.Game;
using CognifyAntiCheat.Listener.Event.Impl.GSManager;
using CognifyAntiCheat.Listener.Event.Impl.ICutscene;
using CognifyAntiCheat.Listener.Event.Impl.Player;
using CognifyAntiCheat.Listener.Event.Impl.RManager;
using CognifyAntiCheat.Listener.Event.Impl.VentImpl;
using HarmonyLib;
using Il2CppSystem.Collections.Generic;

namespace CognifyAntiCheat.Listener.Patches;

[HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.CoBegin))]
internal class CoBeginPatch
{
    public static bool Prefix(IntroCutscene __instance)
    {
        return ListenerManager.GetManager()
            .ExecuteHandlers(new IntroCutsceneCoBeginEvent(__instance), EventHandlerType.Prefix);
    }

    public static void Postfix(IntroCutscene __instance)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new IntroCutsceneCoBeginEvent(__instance), EventHandlerType.Postfix);
    }
}

[HarmonyPatch(typeof(IntroCutscene), nameof(IntroCutscene.OnDestroy))]
internal class IntroDestroyPatch
{
    public static bool Prefix(IntroCutscene __instance)
    {
        return ListenerManager.GetManager()
            .ExecuteHandlers(new IntroCutsceneDestroyEvent(__instance), EventHandlerType.Prefix);
    }

    public static void Postfix(IntroCutscene __instance)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new IntroCutsceneDestroyEvent(__instance), EventHandlerType.Postfix);
    }
}

[HarmonyPatch(typeof(AmongUsClient), nameof(AmongUsClient.OnGameEnd))]
internal class EndGamePatch
{
    public static bool Prefix(AmongUsClient __instance, [HarmonyArgument(0)] ref EndGameResult endGameResult)
    {
        var @event = new AmongUsClientGameEndEvent(__instance, endGameResult);
        var result = ListenerManager.GetManager().ExecuteHandlers(@event, EventHandlerType.Prefix);
        endGameResult = @event.GetEndGameResult();
        return result;
    }

    public static void Postfix(AmongUsClient __instance, [HarmonyArgument(0)] ref EndGameResult endGameResult)
    {
        var @event = new AmongUsClientGameEndEvent(__instance, endGameResult);
        var result = ListenerManager.GetManager().ExecuteHandlers(@event, EventHandlerType.Prefix);
        endGameResult = @event.GetEndGameResult();
    }
}

[HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.Start))]
internal class GameStartManagerStartPatch
{
    public static bool Prefix(GameStartManager __instance)
    {
        return ListenerManager.GetManager()
            .ExecuteHandlers(new GameStartManagerStartEvent(__instance), EventHandlerType.Prefix);
    }

    public static void Postfix(GameStartManager __instance)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new GameStartManagerStartEvent(__instance), EventHandlerType.Postfix);
    }
}

[HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.MakePublic))]
internal class MakePublicPatch
{
    public static bool Prefix(GameStartManager __instance)
    {
        return ListenerManager.GetManager()
            .ExecuteHandlers(new GameStartManagerMakePublicEvent(__instance), EventHandlerType.Prefix);
    }

    public static void Postfix(GameStartManager __instance)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new GameStartManagerMakePublicEvent(__instance), EventHandlerType.Postfix);
    }
}

[HarmonyPatch(typeof(RoleManager), nameof(RoleManager.SelectRoles))]
internal class SelectRolesPatch
{
    public static bool Prefix(RoleManager __instance)
    {
        return ListenerManager.GetManager()
            .ExecuteHandlers(new RoleManagerSelectRolesEvent(__instance), EventHandlerType.Prefix);
    }

    public static void Postfix(RoleManager __instance)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new RoleManagerSelectRolesEvent(__instance), EventHandlerType.Postfix);
    }
}

[HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.SetEverythingUp))]
internal class SetEverythingUpPatch
{
    public static bool Prefix(EndGameManager __instance)
    {
        return ListenerManager.GetManager()
            .ExecuteHandlers(new GameSetEverythingUpEvent(__instance), EventHandlerType.Prefix);
    }

    public static void Postfix(EndGameManager __instance)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new GameSetEverythingUpEvent(__instance), EventHandlerType.Postfix);
    }
}

[HarmonyPatch(typeof(ControllerManager), nameof(ControllerManager.Update))]
internal class ControllerManagerPatch
{
    public static bool Prefix(ControllerManager __instance)
    {
        return ListenerManager.GetManager()
            .ExecuteHandlers(new ControllerManagerUpdateEvent(__instance), EventHandlerType.Prefix);
    }

    public static void Postfix(ControllerManager __instance)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new ControllerManagerUpdateEvent(__instance), EventHandlerType.Postfix);
    }
}

[HarmonyPatch(typeof(Vent), nameof(Vent.CanUse))]
public static class PlayerVentPatch
{
    [HarmonyPrefix]
    public static bool Prefix(Vent __instance,
        [HarmonyArgument(0)] NetworkedPlayerInfo playerInfo,
        [HarmonyArgument(1)] ref bool canUse,
        [HarmonyArgument(2)] ref bool couldUse,
        ref float __result)
    {
        var @event = new VentCheckEvent(__instance, playerInfo, canUse, couldUse, __result);
        var result = ListenerManager.GetManager()
            .ExecuteHandlers(@event, EventHandlerType.Prefix);
        canUse = @event.GetCanUse();
        couldUse = @event.GetCouldUse();
        __result = @event.GetResult();
        return result;
    }

    [HarmonyPostfix]
    public static void Postfix(Vent __instance,
        [HarmonyArgument(0)] NetworkedPlayerInfo playerInfo,
        [HarmonyArgument(1)] ref bool canUse,
        [HarmonyArgument(2)] ref bool couldUse,
        ref float __result)
    {
        var @event = new VentCheckEvent(__instance, playerInfo, canUse, couldUse, __result);
        ListenerManager.GetManager()
            .ExecuteHandlers(@event, EventHandlerType.Postfix);
        canUse = @event.GetCanUse();
        couldUse = @event.GetCouldUse();
        __result = @event.GetResult();
    }
}

[HarmonyPatch(typeof(LogicGameFlowNormal), nameof(LogicGameFlowNormal.CheckEndCriteria))]
internal class GameEndChecker
{
    [HarmonyPrefix]
    public static bool Prefix(LogicGameFlowNormal __instance)
    {
        return ListenerManager.GetManager().ExecuteHandlers(new GameCheckEndEvent(__instance), EventHandlerType.Prefix);
    }

    [HarmonyPostfix]
    public static void Postfix(LogicGameFlowNormal __instance)
    {
        ListenerManager.GetManager().ExecuteHandlers(new GameCheckEndEvent(__instance), EventHandlerType.Postfix);
    }
}

[HarmonyPatch(typeof(GameManager), nameof(GameManager.CheckTaskCompletion))]
internal class CheckTaskCompletionPatch
{
    public static bool Prefix(GameManager __instance, ref bool __result)
    {
        var @event = new GameCheckTaskCompletionEvent(__instance, __result);
        var result = ListenerManager.GetManager().ExecuteHandlers(@event,
            EventHandlerType.Prefix);
        __result = @event.GetResult();
        return result;
    }

    public static void Postfix(GameManager __instance, ref bool __result)
    {
        var @event = new GameCheckTaskCompletionEvent(__instance, __result);
        ListenerManager.GetManager().ExecuteHandlers(@event,
            EventHandlerType.Postfix);
        __result = @event.GetResult();
    }
}

[HarmonyPatch(typeof(MapBehaviour), nameof(MapBehaviour.ShowSabotageMap))]
internal class SabotageMapOpen
{
    private static bool Prefix(MapBehaviour __instance)
    {
        return ListenerManager.GetManager()
            .ExecuteHandlers(new GameShowSabotageMapEvent(__instance), EventHandlerType.Prefix);
    }

    private static void Postfix(MapBehaviour __instance)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new GameShowSabotageMapEvent(__instance), EventHandlerType.Postfix);
    }
}

[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CoSetTasks))]
internal class TaskPatch
{
    public static bool Prefix(PlayerControl __instance, ref List<NetworkedPlayerInfo> tasks)
    {
        var typeTasks = tasks;
        var result = ListenerManager.GetManager()
            .ExecuteHandlers(new PlayerCoSetTasksEvent(__instance, typeTasks), EventHandlerType.Prefix);
        tasks = typeTasks;
        return result;
    }

    public static void Postfix(PlayerControl __instance, ref List<NetworkedPlayerInfo> tasks)
    {
        var typeTasks = tasks;
        ListenerManager.GetManager()
            .ExecuteHandlers(new PlayerCoSetTasksEvent(__instance, typeTasks), EventHandlerType.Postfix);
        tasks = typeTasks;
    }
}

[HarmonyPatch(typeof(GameManager), nameof(GameManager.StartGame))]
internal class GameStartPatch
{
    public static bool Prefix(GameManager __instance)
    {
        return ListenerManager.GetManager().ExecuteHandlers(new GameStartEvent(__instance), EventHandlerType.Prefix);
    }

    public static void Postfix(GameManager __instance)
    {
        ListenerManager.GetManager().ExecuteHandlers(new GameStartEvent(__instance), EventHandlerType.Postfix);
    }
}

[HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.Update))]
public class GameStartManagerUpdatePatch
{
    [HarmonyPrefix]
    public static bool Prefix(GameStartManager __instance)
    {
        return ListenerManager.GetManager()
            .ExecuteHandlers(new GameStartManagerUpdateEvent(__instance), EventHandlerType.Prefix);
    }

    [HarmonyPostfix]
    public static void Postfix(GameStartManager __instance)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new GameStartManagerUpdateEvent(__instance), EventHandlerType.Postfix);
    }
}

[HarmonyPatch(typeof(GameStartManager), nameof(GameStartManager.BeginGame))]
public class GameStartManagerBeginGamePatch
{
    [HarmonyPrefix]
    public static bool Prefix(GameStartManager __instance)
    {
        return ListenerManager.GetManager()
            .ExecuteHandlers(new GameStartManagerBeginGameEvent(__instance), EventHandlerType.Prefix);
    }

    [HarmonyPostfix]
    public static void Postfix(GameStartManager __instance)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new GameStartManagerBeginGameEvent(__instance), EventHandlerType.Postfix);
    }
}

[HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.CompleteTask))]
public class PlayerControlCompleteTaskPatch
{
    public static bool Prefix(PlayerControl __instance, uint idx)
    {
        return ListenerManager.GetManager()
            .ExecuteHandlers(new PlayerTaskFinishEvent(__instance, idx), EventHandlerType.Prefix);
    }

    public static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] uint idx)
    {
        ListenerManager.GetManager()
            .ExecuteHandlers(new PlayerTaskFinishEvent(__instance, idx), EventHandlerType.Postfix);
    }
}

[HarmonyPatch(typeof(ExileController), nameof(ExileController.Begin))]
public class ExileControllerBeginPatch
{
    public static bool Prefix(ExileController __instance, NetworkedPlayerInfo exiled, bool tie)
    {
        return ListenerManager.GetManager()
            .ExecuteHandlers(new PlayerExileBeginEvent(exiled?.Object, __instance, exiled, tie),
                EventHandlerType.Prefix);
    }

    public static void Postfix(ExileController __instance, [HarmonyArgument(0)] NetworkedPlayerInfo exiled,
        [HarmonyArgument(1)] bool tie)
    {
        ListenerManager.GetManager().ExecuteHandlers(new PlayerExileBeginEvent(exiled?.Object, __instance, exiled, tie),
            EventHandlerType.Postfix);
    }
}