using InnerNet;
using AmongUs.GameOptions;

namespace CognifyAntiCheat.States;

/// <summary>
///     与游戏相关状态
/// </summary>
public static class GameStates
{
    /// <summary>
    ///     是否处于游戏中
    /// </summary>
    public static bool InGame => AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Started;

    /// <summary>
    ///     是否在大厅中
    /// </summary>
    public static bool IsLobby => AmongUsClient.Instance.GameState == InnerNetClient.GameStates.Joined;

    /// <summary>
    ///     是否处于会议中
    /// </summary>
    public static bool IsMeeting => InGame && MeetingHud.Instance;

    public static bool IsOnlineGame => AmongUsClient.Instance.NetworkMode == NetworkModes.OnlineGame;

    /// <summary>
    ///     是否处于投票阶段
    /// </summary>
    public static bool IsVoting => IsMeeting &&
                                   MeetingHud.Instance.state is MeetingHud.VoteStates.Voted
                                       or MeetingHud.VoteStates.NotVoted;
    
    /// <summary>
    ///     是否是躲猫猫模式
    /// </summary>
    public static bool isHideNSeek => GameOptionsManager.Instance.CurrentGameOptions.GameMode == GameModes.HideNSeek;
    
    /// <summary>
    ///     获取真实名
    /// </summary>
    public static string GetRealName(this PlayerControl player, bool isMeeting = false)
    {
        return isMeeting ? player?.Data?.PlayerName : player?.name;
    }
}