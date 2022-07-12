using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _maxCombo;
    [SerializeField] TextMeshProUGUI _heightTMP;

    private void Start()
    {
        //_heightTMP.text = $"{PlayerController.Instance.Height:0.0}m";

        Fade.Instance.FadeIn();
        //_maxCombo.text = 
    }
}
