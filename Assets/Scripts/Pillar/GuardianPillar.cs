using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardianPillar : Pillar
{
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

        UIManager.Instance.TimingSlider.gameObject.SetActive(true);
        UIManager.Instance.TimingSlider.StartMove();

        while (true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                break;
            }
            yield return null;
        }

        UIManager.Instance.TimingSlider.StopMove();
        UIManager.Instance.TimingSlider.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
