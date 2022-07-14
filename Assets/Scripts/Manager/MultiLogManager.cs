using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

public class MultiLogManager : MonoSingleton<MultiLogManager>
{
    private string _nameCache = "";

    private void Awake()
    {
        ServerManager.Instance.OnJoin += OnJoin;
        ServerManager.Instance.OnLeave += OnLeave;
        ServerManager.Instance.OnLeaderboard += OnLeaderboard;
    }

    private void OnJoin(ServerManager.JoinPacket joinPacket)
    {
        Log($"{joinPacket.Name}���� �����ϼ̽��ϴ�.");
    }

    private void OnLeave(ServerManager.LeavePacket leavePacket)
    {
        Log($"{RealtimeLeaderboardManager.Instance.GetNameById(leavePacket.Id)}���� ������ �����̽��ϴ�.");
    }

    private void OnLeaderboard(ServerManager.RealtimeLeaderboardPacket leaderboardPacket)
    {
        if (RealtimeLeaderboardManager.Instance.GetFirstEntry() == null) return;
        if (_nameCache != RealtimeLeaderboardManager.Instance.GetFirstEntry().Name)
        {
            Log($"{RealtimeLeaderboardManager.Instance.GetFirstEntry().Name}���� ���θ� �޸��� �ֽ��ϴ�.");
            _nameCache = RealtimeLeaderboardManager.Instance.GetFirstEntry().Name;
        }
    }

    public void Log(string message)
    {
        var activedEntrys = PoolManager<MultiLogEntry>.GetAllActive();

        int count = 0;
        foreach (var entry in activedEntrys)
        {
            entry.EntryCreated();
            count++;
        }

        var enrtyObject = PoolManager<MultiLogEntry>.Get(transform);
        enrtyObject.MessageText.text = message;
        (enrtyObject.transform as RectTransform).anchoredPosition = new Vector2(-500, 0);
    }
}
