using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingSlider : MonoBehaviour
{
    [SerializeField]
    private float _valueSpeed = 2f;

    private Slider _slider = null;

    void Start()
    {
        _slider = GetComponent<Slider>();
        StartMove();
    }

    public void StartMove()
    {
        StartCoroutine(nameof(MoveValueCoroutine));
    }

    public float StopMove()
    {
        StopCoroutine(nameof(MoveValueCoroutine));
        return _slider.value;
    }

    private IEnumerator MoveValueCoroutine()
    {
        int i = 1;
        while (true)
        {
            _slider.value += _valueSpeed * i;
            if (_slider.value >= _slider.maxValue)
            {
                i = -1;
            }
            else if (_slider.value <= _slider.minValue)
            {
                i = 1;
            }
            yield return null;
        }
    }
}
