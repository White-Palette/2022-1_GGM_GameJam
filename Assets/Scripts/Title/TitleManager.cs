using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Image startImage;
    [SerializeField] GameObject settingPanel;

    private bool isEnable = false;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleSettingPanel();
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
        ToggleSettingPanel();
    }

    public void ToggleSettingPanel()
    {
        settingPanel.SetActive(!settingPanel.activeSelf);

        if (settingPanel.activeSelf)
        {
            isEnable = true;
        }
        if (!settingPanel.activeSelf)
        {
            isEnable = false;
        }

        Debug.Log("isEnable : " + isEnable);
    }

    public void GameStart()
    {
        Debug.Log("Game Start");
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }
}
