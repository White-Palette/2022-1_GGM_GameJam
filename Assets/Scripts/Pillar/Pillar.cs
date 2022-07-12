using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

[Serializable]
public class Range
{
    public float min;
    public float max;

    public Range(float min, float max)
    {
        this.min = min;
        this.max = max;
    }
}

public class Pillar : MonoBehaviour, IPoolable
{
    public Pillar LeftPillar;
    public Pillar RightPillar;
    public Range HorizontalRange = new Range(3, 5);
    public Range VerticalRange = new Range(3, 5);
    public Color Color
    {
        get
        {
            return SpriteRenderer.color;
        }
        set
        {
            SpriteRenderer.color = value;
        }
    }

    [HideInInspector]
    public SpriteRenderer SpriteRenderer;

    private MapContainer _mapContainer = null;

    protected virtual void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        _mapContainer = Resources.Load<MapContainer>("MapContainer");
    }

    public virtual void TowerEvent()
    {
        Debug.Log("Default");
    }

    public virtual void Generate()
    {
        if (LeftPillar != null || RightPillar != null)
            return;

        var map = _mapContainer.GetPillarMap();

        Vector2 rightPillarPosition = transform.position + Vector3.right * Random.Range(HorizontalRange.min, HorizontalRange.max);
        rightPillarPosition = rightPillarPosition + Vector2.up * Random.Range(VerticalRange.min, VerticalRange.max);
        RightPillar = TowerGenerator.Instance.GenerateTower(transform.parent, rightPillarPosition, map.RightPillarType);

        Vector2 leftPillarPosition = transform.position + Vector3.left * Random.Range(HorizontalRange.min, HorizontalRange.max);
        leftPillarPosition = leftPillarPosition + Vector2.up * Random.Range(VerticalRange.min, VerticalRange.max);
        LeftPillar = TowerGenerator.Instance.GenerateTower(transform.parent, leftPillarPosition, map.LeftPillarType);
    }

    protected virtual void Update()
    {
        if (transform.position.y - Camera.main.transform.position.y < -10f)
        {
            PoolManager<Pillar>.Release(this, true);
        }
    }

    public virtual void Initialize()
    {
        LeftPillar = null;
        RightPillar = null;
        Color = Color.white;
        transform.DOMoveY(transform.position.y, 0.2f).From(transform.position.y - 1f);
        SpriteRenderer.DOFade(1f, 0.2f).From(0f);
    }
}
