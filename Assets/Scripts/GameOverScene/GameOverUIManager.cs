using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class GameOverUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _maxCombo;
    [SerializeField] TextMeshProUGUI _heightTMP;

    [SerializeField] Light2D light2d;

    private bool isLoading = false;

    private void Start()
    {
        //_heightTMP.text = $"{UserData.Cache.Height:0.0}m";
        //_maxCombo.text = $"{UserData.Cache.MaxCombo}";
        isLoading = false;
        light2d.intensity = UserData.Brightness;
        Fade.Instance.FadeIn();
        StartCoroutine(HeightRecords());
    }

    private void Update()
    {
        KeyDown();
    }
    void KeyDown()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isLoading)
        {
            isLoading = true;
            Fade.Instance.FadeOutToGameScene();
        }
    }

    IEnumerator HeightRecords()
    {
        int i = 0;
        float randomHeight = 0;
        int randomCombo = 0;

        while(i < 35)
        {
            if (Input.anyKeyDown || Input.GetMouseButtonDown(0))
                break;

            randomHeight = Random.Range(0f, 999f);
            _heightTMP.text = $"{randomHeight:0.0}m";

            randomCombo = Random.Range(0, 200);
            _maxCombo.text = $"{randomCombo}";

            yield return new WaitForSeconds(0.05f);
            ++i;
        }

        _heightTMP.text = $"{UserData.Cache.Height:0.0}m";
        _heightTMP.transform.DOScale(1f, 1.5f).SetEase(Ease.OutCirc).From(3.0f);
        _maxCombo.text = $"{UserData.Cache.MaxCombo}";
        _maxCombo.transform.DOScale(1f, 1.5f).SetEase(Ease.OutCirc).From(3.0f);

    }
}
