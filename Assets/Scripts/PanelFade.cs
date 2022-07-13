using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PanelFade : MonoBehaviour
{
    [SerializeField] GameObject panel;
    private bool isSettingEnable = false;

    public void Panel()
    {
        StartCoroutine(TogglePanel(panel));
    }

    IEnumerator TogglePanel(GameObject Panel)
    {
        if (isSettingEnable)
        {
            Panel.transform.DOScale(new Vector3(0f, 0f, 0f), 0.3f).From(1f);
            yield return new WaitForSeconds(0.3f);
            Panel.SetActive(!Panel.activeSelf);
        }
        if (!isSettingEnable)
        {
            Panel.SetActive(!Panel.activeSelf);
            Panel.transform.DOScale(new Vector3(0.9f, 0.9f, 0f), 1f).SetEase(Ease.OutBounce).From(0f);
        }
        isSettingEnable = !isSettingEnable;
        yield break;
    }
}
