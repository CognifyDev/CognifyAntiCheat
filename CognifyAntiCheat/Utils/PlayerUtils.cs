using System.Collections.Generic;
using System.Linq;
using AmongUs.Data;
using Hazel;
using InnerNet;
using UnityEngine;
using GameStates = CognifyAntiCheat.States.GameStates;

namespace CognifyAntiCheat.Utils;

public enum ColorType
{
    Unknown = -1,
    Light,
    Dark
}

public static class PlayerUtils
{
    public static readonly int Outline = Shader.PropertyToID("_Outline");
    public static readonly int OutlineColor = Shader.PropertyToID("_OutlineColor");
    public static PoolablePlayer? PoolablePlayerPrefab { get; set; }

    /// <summary>
    ///     获取距离目标玩家位置最近的玩家
    /// </summary>
    /// <param name="target">目标玩家</param>
    /// <param name="mustAlive">是否必须为活着的玩家</param>
    /// <returns>最近位置的玩家</returns>
    public static PlayerControl? GetClosestPlayer(this PlayerControl target, bool mustAlive = true,
        float closestDistance = float.MaxValue)
    {
        var targetLocation = target.GetTruePosition();
        var players = mustAlive ? GetAllAlivePlayers() : GetAllPlayers();

        PlayerControl? closestPlayer = null;

        foreach (var player in players)
        {
            if (player == target) continue;

            var playerLocation = player.GetTruePosition();
            var distance = Vector2.Distance(targetLocation, playerLocation);

            if (distance >= closestDistance) continue;
            closestDistance = distance;
            closestPlayer = player;
        }

        return closestPlayer;
    }

    public static List<PlayerControl> GetAllPlayers()
    {
        return new List<PlayerControl>(PlayerControl.AllPlayerControls.ToArray().Where(p => p));
    }

    public static List<PlayerControl> GetAllAlivePlayers()
    {
        return GetAllPlayers().ToArray().Where(player => player != null && player.IsAlive()).ToList();
    }

    public static bool IsSamePlayer(this NetworkedPlayerInfo info, NetworkedPlayerInfo target)
    {
        return IsSamePlayer(info.Object, target.Object);
    }

    public static bool IsSamePlayer(this PlayerControl player, PlayerControl target)
    {
        try
        {
            return player.PlayerId == target.PlayerId;
        }
        catch
        {
            return false;
        }
    }

    public static void SetNamePrivately(PlayerControl target, PlayerControl seer, string name)
    {
        var writer =
            AmongUsClient.Instance.StartRpcImmediately(target.NetId, (byte)RpcCalls.SetName, SendOption.Reliable,
                seer.GetClientID());
        writer.Write(name);
        writer.Write(false);
        AmongUsClient.Instance.FinishRpcImmediately(writer);
    }

    public static PlayerControl? GetPlayerById(byte playerId)
    {
        return GetAllPlayers().FirstOrDefault(playerControl => playerControl.PlayerId == playerId);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public static ClientData? GetClient(this PlayerControl player)
    {
        var client = AmongUsClient.Instance.allClients.ToArray()
            .FirstOrDefault(cd => cd.Character.PlayerId == player.PlayerId);
        return client;
    }

    public static int GetClientID(this PlayerControl player)
    {
        return player.GetClient() == null ? -1 : player.GetClient()!.Id;
    }

    /// <summary>
    ///     检测玩家是否存活
    /// </summary>
    /// <param name="player">玩家实例</param>
    /// <returns></returns>
    public static bool IsAlive(this PlayerControl player)
    {
        if (GameStates.IsLobby) return true;
        if (player == null) return false;
        return !player.Data.IsDead;
    }

    public static ColorType GetColorType(this PlayerControl player)
    {
        return player.cosmetics.ColorId switch
        {
            0 or 3 or 4 or 5 or 7 or 10 or 11 or 13 or 14 or 17 => ColorType.Light,
            1 or 2 or 6 or 8 or 9 or 12 or 15 or 16 or 18 => ColorType.Dark,
            _ => ColorType.Unknown
        };
    }
    
    public static void RpcSetNamePrivately(this PlayerControl player, string name, PlayerControl[] targets)
    {
        foreach (var target in targets)
        {
            var clientId = target.GetClientID();
            var writer = AmongUsClient.Instance.StartRpcImmediately(player.NetId, (byte)RpcCalls.SetName,
                SendOption.Reliable, clientId);
            writer.Write(name);
            writer.Write(false);
            AmongUsClient.Instance.FinishRpcImmediately(writer);
        }
    }

    /// <summary>
    ///     设置玩家外观
    /// </summary>
    /// <param name="poolable"></param>
    /// <param name="player"></param>
    public static void SetPoolableAppearance(this PlayerControl player, PoolablePlayer poolable)
    {
        if (!poolable || !player) return;

        var outfit = player.CurrentOutfit;
        var data = player.Data;
        poolable.SetBodyColor(player.cosmetics.ColorId);
        if (data.IsDead) poolable.SetBodyAsGhost();
        poolable.SetBodyType(player.BodyType);
        if (DataManager.Settings.Accessibility.ColorBlindMode) poolable.SetColorBlindTag();

        poolable.SetSkin(outfit.SkinId, outfit.ColorId);
        poolable.SetHat(outfit.HatId, outfit.ColorId);
        poolable.SetName(data.PlayerName);
        poolable.SetFlipX(true);
        poolable.SetBodyCosmeticsVisible(true);
        poolable.SetVisor(outfit.VisorId, outfit.ColorId);

        var names = poolable.transform.FindChild("Names");
        names.localPosition = new Vector3(0, -0.75f, 0);
        names.localScale = new Vector3(1.5f, 1.5f, 1f);
    }

    /// <summary>
    ///     设置角色外侧的线
    ///     比如: 击杀时候的红线
    /// </summary>
    /// <param name="pc">目标玩家</param>
    /// <param name="color">颜色</param>
    public static void SetOutline(this PlayerControl pc, Color color)
    {
        pc.cosmetics.currentBodySprite.BodySprite.material.SetFloat(Outline, 1f);
        pc.cosmetics.currentBodySprite.BodySprite.material.SetColor(OutlineColor, color);
    }

    public static void ClearOutline(this PlayerControl pc)
    {
        pc.cosmetics.currentBodySprite.BodySprite.material.SetFloat(Outline, 0);
    }
}