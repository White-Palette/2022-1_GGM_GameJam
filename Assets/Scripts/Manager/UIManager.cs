using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private TimingSlider _timingSlider = null;

    [SerializeField]
    private TMP_Text _heightText = null;

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
        _timingSlider.gameObject.SetActive(false);
        _heightText.gameObject.SetActive(true);
    }

    private void Update()
    {
        _heightText.text = $"{PlayerController.Instance.Height:0.0}m";
    }
}
