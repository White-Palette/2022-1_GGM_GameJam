using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Image startImage;
    [SerializeField] TextMeshProUGUI startTMP;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject helpPanel;

    private float fadeTime = 2f;

    private void Start()
    {
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
            TogglePanel(settingPanel);
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

    public void TogglePanel(GameObject Panel)
    {
        Panel.SetActive(!Panel.activeSelf);
    }

    public void HelpPanel()
    {
        TogglePanel(helpPanel);
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
