using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimingSlider : MonoBehaviour
{
    public bool IsFail => _isFail;

    [SerializeField]
    private float _valueSpeed = 2f;

    private Slider _slider = null;

    private bool _isFail = false;


    void Start()
    {
        _slider = GetComponent<Slider>();
        StartMove();
    }

    public void StartMove()
    {
        _isFail = false;
        _slider.value = 0f;
        _slider.gameObject.SetActive(true);
        StartCoroutine(nameof(MoveValueCoroutine));
    }

    public float StopMove()
    {
        _slider.gameObject.SetActive(false);
        if (IsFail)
        {
            return -1f;
        }

        StopCoroutine(nameof(MoveValueCoroutine));
        return _slider.value;
    }

    private IEnumerator MoveValueCoroutine()
    {
        for (float i = _slider.minValue; i < _slider.maxValue; i += _valueSpeed)
        {
            _slider.value = i;
            yield return null;
        }

        _isFail = true;
    }

    private IEnumerator OldMoveValueCoroutine()
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
