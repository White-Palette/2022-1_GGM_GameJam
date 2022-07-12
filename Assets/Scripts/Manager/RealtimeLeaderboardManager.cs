using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class RealtimeLeaderboardManager : MonoSingleton<RealtimeLeaderboardManager>
{
    [SerializeField] Transform realtimeLeaderboardTransform = null;

    private Dictionary<int, RealtimeLeaderboardEntry> realtimeLeaderboard = new Dictionary<int, RealtimeLeaderboardEntry>();

    private void Awake()
    {
        ServerManager.Instance.OnJoin += OnJoin;

        ServerManager.Instance.OnLeave += OnLeave;

        ServerManager.Instance.OnLeaderboard += OnLeaderboard;
    }

    private void OnJoin(ServerManager.JoinPacket packet)
    {
        var leaderboardEntry = PoolManager<RealtimeLeaderboardEntry>.Get(realtimeLeaderboardTransform);
        leaderboardEntry.Name = packet.Name;
        leaderboardEntry.Height = 0;
        ColorUtility.TryParseHtmlString(packet.Color, out Color color);
        leaderboardEntry.Color = color;
        Debug.Log($"Color: {packet.Color}");
        realtimeLeaderboard.Add(packet.Id, leaderboardEntry);
    }

    private void OnLeave(ServerManager.LeavePacket packet)
    {
        if (realtimeLeaderboard.ContainsKey(packet.Id))
        {
            PoolManager<RealtimeLeaderboardEntry>.Release(realtimeLeaderboard[packet.Id]);
            realtimeLeaderboard.Remove(packet.Id);
        }
    }

    private void OnLeaderboard(ServerManager.RealtimeLeaderboardPacket packet)
    {
        float maxHeight = 0;
        float transformHeight = (realtimeLeaderboardTransform as RectTransform).rect.height;

        foreach (var entry in packet.Leaderboard)
        {
            if (realtimeLeaderboard.ContainsKey(entry.Id))
            {
                realtimeLeaderboard[entry.Id].Height = entry.Height;
                if (entry.Height > maxHeight)
                {
                    maxHeight = entry.Height;
                }
            }
        }

        float scale = transformHeight / maxHeight;
        if (maxHeight <= 0)
        {
            scale = 0;
        }

        foreach (var entry in packet.Leaderboard)
        {
            if (realtimeLeaderboard.ContainsKey(entry.Id))
            {
                realtimeLeaderboard[entry.Id].Height = entry.Height;
                (realtimeLeaderboard[entry.Id].transform as RectTransform).DOAnchorPosY(entry.Height * scale, 0.5f);
            }
        }
    }
}