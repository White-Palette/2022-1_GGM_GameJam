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
    [SerializeField] GameObject gameQuitPanel;

    private float fadeTime = 2f;

    private bool isSettingEnable = false;
    private bool isHelpEnable = false;
    private bool isGameQuitEnable = false;

    private bool isLoading = false;

    private void Start()
    {
        settingPanel.transform.localScale = new Vector3(0, 0, 0);
        StartCoroutine(FadeInOut());
        Fade.Instance.FadeIn();
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
            yield return StartCoroutine(FadeTMP(1, 0.3f));

            yield return StartCoroutine(FadeTMP(0.3f, 1));
        }
    }

    private IEnumerator FadeTMP(float start, float end)
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
            SettingPanel();
            //ToggleSettingPanel();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            GameQuitPanel();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            DisableAllPanel();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            return;
        }
        else if (Input.anyKeyDown)
        {
            if (!isLoading)
            {
                Fade.Instance.FadeOutToGameScene();
                isLoading = !isLoading;
            }
        }
    }

    public void DisableAllPanel()
    {
        SoundManager.Instance.PlaySound(Effect.Click);
        if (isSettingEnable)
        {
            SettingPanel();
        }
        if (isHelpEnable)
        {
            HelpPanel();
        }
        if (isGameQuitEnable)
        {
            GameQuitPanel();
        }
    }

    IEnumerator TogglePanel(GameObject Panel, bool isEnable)
    {
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
        isHelpEnable = !isHelpEnable;
        SoundManager.Instance.PlaySound(Effect.Click);
        StartCoroutine(TogglePanel(helpPanel, isHelpEnable));
    }

    public void SettingPanel()
    {
        isSettingEnable = !isSettingEnable;
        SoundManager.Instance.PlaySound(Effect.Click);
        StartCoroutine(TogglePanel(settingPanel, isSettingEnable));
    }

    public void GameQuitPanel()
    {
        isGameQuitEnable = !isGameQuitEnable;
        SoundManager.Instance.PlaySound(Effect.Click);
        StartCoroutine(TogglePanel(gameQuitPanel, isGameQuitEnable));
    }

    public void GameStart()
    {
        Debug.Log("Game Start");
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        SoundManager.Instance.PlaySound(Effect.Click);
        Application.Quit();
        Debug.Log("Quit");
    }
}
