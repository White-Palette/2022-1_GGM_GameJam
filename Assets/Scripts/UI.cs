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
        setting.SetActive(!setting.activeSelf);

        settingPanel.transform.DOScale(new Vector3(1f, 1f, 0), 0.6f).SetEase(Ease.OutBounce);
        yield return null;
    }

}
