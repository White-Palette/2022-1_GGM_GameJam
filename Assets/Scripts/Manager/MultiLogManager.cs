using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MultiLogManager : MonoSingleton<MultiLogManager>
{
    [SerializeField] TextMeshProUGUI[] logTMP;

    private string firstEntryName;

    private void Awake()
    {
        ServerManager.Instance.OnLeave += leavePacket =>
        {
            GameManager.Instance.UpdateDeadLog(leavePacket.Id);
        };
    }

    private void Update()
    {
        if(RealtimeLeaderboardManager.Instance.GetFirstEntry() != null)
        {
            logTMP[0].text = $"{RealtimeLeaderboardManager.Instance.GetFirstEntry().Name} ��(��) ���θ� �޸��� �ֽ��ϴ�!";
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            logTMP[2].rectTransform.DOAnchorPosX(450f, 1f);
        }

        ChangeFirstEntry();
    }

    public IEnumerator MoveAndFadeTMP(int tmp)
    {
        logTMP[tmp].DOFade(1f, 1f).SetEase(Ease.OutQuad).From(0f);
        logTMP[tmp].rectTransform.DOAnchorPosX(450f, 1f);
        yield return new WaitForSeconds(2f);
        logTMP[tmp].DOFade(0f, 1f).SetEase(Ease.OutQuad).From(1f);
        logTMP[tmp].rectTransform.DOAnchorPosX(-400f, 1f);
        yield return new WaitForSeconds(1f);
        yield break;
    }

    public void DeadLog(int tmp, string name)
    {
        Debug.Log(name + "��(��) ���������ϴ�.");
        logTMP[tmp].text = $"{name} ��(��) ���������ϴ�.";
        StartCoroutine(MoveAndFadeTMP(tmp));
    }

    public void ChangeFirstEntry()
    {
        if (RealtimeLeaderboardManager.Instance.GetFirstEntry() == null) return;
        if (firstEntryName == null && RealtimeLeaderboardManager.Instance.GetFirstEntry() != null)
        {
            firstEntryName = RealtimeLeaderboardManager.Instance.GetFirstEntry().Name;
        }

        if(firstEntryName != RealtimeLeaderboardManager.Instance.GetFirstEntry().Name)
        {
            Debug.Log(RealtimeLeaderboardManager.Instance.GetFirstEntry().Name + " ��(��) ���θ� ���Ѿҽ��ϴ�!");
            firstEntryName = RealtimeLeaderboardManager.Instance.GetFirstEntry().Name;
            logTMP[1].text = $"{RealtimeLeaderboardManager.Instance.GetFirstEntry().Name} ��(��) ���θ� ���Ѿҽ��ϴ�!";
            StartCoroutine(MoveAndFadeTMP(1));
        }
    }
}
