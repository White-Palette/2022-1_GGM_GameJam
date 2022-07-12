using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField]
    private TimingSlider _timingSlider = null;

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
}
