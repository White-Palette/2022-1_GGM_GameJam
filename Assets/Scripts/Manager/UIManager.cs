using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Rendering.Universal;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private TimingSlider _timingSlider = null;

    [SerializeField]
    private TMP_Text _heightText = null;

    [SerializeField]
    private TextMeshProUGUI _comboText = null;

    [SerializeField]
    private Image _PauseImage = null;

    [SerializeField]
    private Light2D light2d;

    [SerializeField]
    private TMP_Text _distanceText = null;

    public TimingSlider TimingSlider
    {
        get
        {
            if (_timingSlider == null)
            {
                _timingSlider = FindObjectOfType<TimingSlider>();

                if (_timingSlider == null)
                {
                    Debug.LogError("TimingSlider is not found.");
                }
            }
            return _timingSlider;
        }
    }

    private IEnumerator Start()
    {
        yield return null;
        light2d.intensity = UserData.Brightness;
        _timingSlider.gameObject.SetActive(false);
        _PauseImage.gameObject.SetActive(false);
        _heightText.gameObject.SetActive(true);
    }

    private void Update()
    {
        _heightText.text = $"{PlayerController.Instance.Height:0.0}m";
        _comboText.text = $"{ComboManager.Instance.Combo} Combo";
        if (ChaserGenerator.Instance.Chaser != null)
        {
            _distanceText.text = $"{ChaserGenerator.Instance.Chaser.Distance:0.0}m";
        }
        else
        {
            _distanceText.text = "";
        }
    }



    public void SetPauseImage(bool isActive)
    {
        _PauseImage.gameObject.SetActive(isActive);
    }

    public IEnumerator ComboEffect()
    {
        _comboText.transform.DOScale(new Vector2(1.2f, 1.2f), 0.2f).SetEase(Ease.OutSine);
        yield return new WaitForSeconds(0.2f);
        _comboText.transform.DOScale(new Vector2(1f, 1f), 0.2f).SetEase(Ease.OutSine);

        yield break;
    }
}
