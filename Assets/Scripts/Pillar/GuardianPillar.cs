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

    public override void Initialize()
    {
        base.Initialize();
        if (transform.position.x < PlayerController.Instance.transform.position.x)
            _guardian.transform.localScale = new Vector3(1.2f, 1.2f, 1);
        else
            _guardian.transform.localScale = new Vector3(-1.2f, 1.2f, 1);
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
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                break;
            }
            yield return null;
        }

        _inputValue = UIManager.Instance.TimingSlider.StopMove();

        if (_inputValue == -1f)
        {
            Debug.Log("Fail2");
            _guardian.Attack2();
        }
        else

        if (_inputValue < 35f || _inputValue > 65f)
        {
            Debug.Log("Fail1");
            _guardian.Attack1();
        }

        else
        {
            Debug.Log("Success");
            _guardian.Hit();
        }

        Time.timeScale = 1f;
    }
}
