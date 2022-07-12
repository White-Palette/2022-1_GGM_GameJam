using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Fade : MonoSingleton<Fade>
{
    [SerializeField] Image fadeImg;

    int sceneLoad = 0;

    private void Start()
    {
        FadeIn();
    }

    public void FadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    public IEnumerator FadeInCoroutine()
    {
        fadeImg.fillOrigin = 0;
        fadeImg.DOFillAmount(0f, 1f).SetEase(Ease.InQuad).From(1f);
        yield return new WaitForSeconds(1f);
        fadeImg.raycastTarget = false;

    }

    public void FadeOutToGameScene()
    {
        sceneLoad = 1;
        StartCoroutine(FadeOutCoroutine(sceneLoad));
    }

    public void FadeOutToMainMenu()
    {
        sceneLoad = 0;
        StartCoroutine(FadeOutCoroutine(sceneLoad));
    }

    public IEnumerator FadeOutCoroutine(int sceneLoad)
    {
        fadeImg.fillOrigin = 1;
        fadeImg.DOFillAmount(1f, 1f).SetEase(Ease.InQuad).From(0f);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneLoad);
        yield break;
    }
}
