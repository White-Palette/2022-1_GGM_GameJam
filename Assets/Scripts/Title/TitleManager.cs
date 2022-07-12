using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Image startImage;
    [SerializeField] TextMeshProUGUI startTMP;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject helpPanel;

    private float fadeTime = 2f;

    private bool isEnable = false;

    private void Start()
    {
        DisableAllPanel();
        settingPanel.transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(FadeInOut());
    }

    private void Update()
    {
        KeyDown();
    }

    #region Fade
    IEnumerator FadeInOut()
    {
        while (true)
        {
            yield return StartCoroutine(Fade(1, 0.3f));

            yield return StartCoroutine(Fade(0.3f, 1));
        }
    }

    private IEnumerator Fade(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while (percent < 1)
        {
            current += Time.deltaTime;
            percent = current / fadeTime;

            Color color = startTMP.color;
            color.a = Mathf.Lerp(start, end, percent);
            startTMP.color = color;

            yield return null;
        }
    }
    #endregion

    public void KeyDown()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(TogglePanel(settingPanel));
            //ToggleSettingPanel();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            Quit();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            return;
        }
        else if (Input.anyKeyDown)
        {
            GameStart();
        }
    }

    public void DisableAllPanel()
    {
        //ToggleSettingPanel();
        settingPanel.SetActive(false);
        helpPanel.SetActive(false);
    }

    //public void ToggleSettingPanel()
    //{
    //    settingPanel.SetActive(!settingPanel.activeSelf);

    //    if (settingPanel.activeSelf)
    //    {
    //        isEnable = true;
    //    }
    //    if (!settingPanel.activeSelf)
    //    {
    //        isEnable = false;
    //    }

    //    Debug.Log("isEnable : " + isEnable);
    //}

    IEnumerator TogglePanel(GameObject Panel)
    {
        isEnable = !isEnable;

        if (!isEnable)
        {
            Panel.transform.DOScale(new Vector3(0f, 0f, 0f), 0.2f);
            yield return new WaitForSeconds(0.2f);
        }

        Panel.SetActive(!Panel.activeSelf);

        if (isEnable)
        {
            Panel.transform.DOScale(new Vector3(1f, 1f, 0f), 0.6f).SetEase(Ease.OutBounce);
        }

        yield break;
    }

    public void HelpPanel()
    {
        StartCoroutine(TogglePanel(helpPanel));
    }

    public void SettingPanel()
    {
        StartCoroutine(TogglePanel(settingPanel));
    }

    public void GameStart()
    {
        Debug.Log("Game Start");
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
