using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianPillar : Pillar
{
    private Guardian _guardian = null;

    float _inputValue = 0f;

    protected override void Awake()
    {
        base.Awake();
        _guardian = GetComponentInChildren<Guardian>();
    }

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

        if (_inputValue == -1f)
        {
            Debug.Log("Fail");
        }
        else

        if (_inputValue < 35f || _inputValue > 65f)
        {
            Debug.Log("Fail2");
        }

        else
        {
            Debug.Log("Success");
        }

        Time.timeScale = 1f;
    }
}
