using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class MultiLogEntry : MonoBehaviour, IPoolable
{
    public TMP_Text MessageText;
    public bool Exiting = true;

    private int _index = -1;

    public void Initialize()
    {
        Exiting = true;
        (transform as RectTransform).DOAnchorPosX(0, 0.5f).OnComplete(() =>
        {
            Exiting = false;
            Invoke("Hide", 3f);
        });

        _index = -1;
    }

    public void Hide()
    {
        Exiting = true;
        (transform as RectTransform).DOAnchorPosX(-500, 0.5f).OnComplete(() =>
        {
            PoolManager<MultiLogEntry>.Release(this);
        });
    }

    public void EntryCreated()
    {
        _index++;
        (transform as RectTransform).DOAnchorPosY(-40 * _index, 0.5f, true);
    }
}