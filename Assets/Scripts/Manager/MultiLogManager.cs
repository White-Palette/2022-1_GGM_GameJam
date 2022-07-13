using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class MultiLogManager : MonoSingleton<MultiLogManager>
{
    [SerializeField] TextMeshProUGUI[] logTMP;

    private void Awake()
    {
        ServerManager.Instance.OnLeave += leavePacket =>
        {
            GameManager.Instance.UpdateDeadLog();
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

    public void DeadLog(int tmp)
    {
        Debug.Log(RealtimeLeaderboardManager.Instance.GetFirstEntry().Name + "��(��) ���������ϴ�.");
        //logTMP[tmp].text = $"{RealtimeLeaderboardManager.Instance.GetNameById()} ��(��) ���������ϴ�.";
        StartCoroutine(MoveAndFadeTMP(tmp));
    }
}
