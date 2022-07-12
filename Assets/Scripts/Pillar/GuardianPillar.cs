using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianPillar : Pillar
{
    float _inputValue = 0f;

    public override void TowerEvent()
    {
        Debug.Log("Duel");
        StartCoroutine(nameof(DuelCoroutine));
    }

    private IEnumerator DuelCoroutine()
    {
        while (Vector3.Distance(transform.position + Vector3.up * 1.7f, PlayerController.Instance.transform.position) > 2f)
        {
            yield return null;
        }

        Time.timeScale = 0.1f;

        UIManager.Instance.TimingSlider.StartMove();

        while (!UIManager.Instance.TimingSlider.IsFail)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                break;
            }
            yield return null;
        }

        _inputValue = UIManager.Instance.TimingSlider.StopMove();
        // TODO : 판정 해주는 코드 추가 -1이면 실패
        Time.timeScale = 1f;
    }
}
