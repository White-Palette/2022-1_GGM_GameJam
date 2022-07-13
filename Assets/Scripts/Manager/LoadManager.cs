using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            UserData.UserName = inputField.text;
            if(inputField.text == null)
            {
                UserData.UserName = "Guest";
            }
            Fade.Instance.FadeOutToMainMenu();
        }
    }
}