using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject setting;
    [SerializeField] GameObject settingPanel;

    bool isEnable = false;

    private void Start()
    {
        setting.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(ToggleSettingPanel());
        }
    }

    IEnumerator ToggleSettingPanel()
    {
        isEnable = !isEnable;

        if (!isEnable)
        {
            settingPanel.transform.DOScale(new Vector3(0f, 0f, 0f), 0.2f);
            yield return new WaitForSeconds(0.2f);
        }

        setting.SetActive(!setting.activeSelf);

        if (isEnable)
        {
            settingPanel.transform.DOScale(new Vector3(1f, 1f, 0f), 0.6f).SetEase(Ease.OutBounce);
        }

        yield break;
    }

}
