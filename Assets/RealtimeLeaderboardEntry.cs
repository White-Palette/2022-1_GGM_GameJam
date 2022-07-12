using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RealtimeLeaderboardEntry : MonoBehaviour, IPoolable
{
    public void Initialize()
    {
        Color = Color.white;
        NameText.text = "";
        HeightText.text = "";
    }

    [SerializeField] TMP_Text NameText = null;
    [SerializeField] TMP_Text HeightText = null;
    private float _height = 0;

    public string Name
    {
        get => NameText.text;
        set => NameText.text = value;
    }

    public float Height
    {
        get => _height;
        set
        {
            DOTween.To(() => _height, x => _height = x, value, 0.1f);
            HeightText.text = _height.ToString("0.0") + "m";
        }
    }

    public Color Color 
    {
        get => image.color;
        set => image.color = value;
    }

    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
}
