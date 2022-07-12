using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] Pillar currentPillar;
    [SerializeField] Color nextPillarColor = Color.white;
    [SerializeField] Color currentPillarColor = Color.white;
    [SerializeField] Color previousPillarColor = Color.white;
    [SerializeField] AnimationCurve jumpCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private bool isMoving = false;

    private void Start()
    {
        MoveToPillar(currentPillar);
    }

    private void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (currentPillar.LeftPillar != null)
                {
                    MoveToPillar(currentPillar.LeftPillar);
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (currentPillar.RightPillar != null)
                {
                    MoveToPillar(currentPillar.RightPillar);
                }
            }
        }
    }

    public void MoveToPillar(Pillar pillar)
    {
        isMoving = true;

        if (currentPillar.LeftPillar != null)
        {
            currentPillar.LeftPillar.SpriteRenderer.DOColor(Color.white, 0.2f);
        }
        if (currentPillar.RightPillar != null)
        {
            currentPillar.RightPillar.SpriteRenderer.DOColor(Color.white, 0.2f);
        }

        currentPillar.SpriteRenderer.DOColor(previousPillarColor, 0.2f);
        currentPillar = pillar;

        currentPillar.TowerEvent();
        currentPillar.Generate();

        transform.DOJump(pillar.transform.position + Vector3.up * 1.7f, 2f, 1, 1f).SetEase(jumpCurve).OnComplete(() =>
        {
            isMoving = false;

            if (currentPillar.LeftPillar != null)
            {
                currentPillar.LeftPillar.SpriteRenderer.DOColor(nextPillarColor, 0.2f);
            }
            if (currentPillar.RightPillar != null)
            {
                currentPillar.RightPillar.SpriteRenderer.DOColor(nextPillarColor, 0.2f);
            }

            currentPillar.SpriteRenderer.DOColor(currentPillarColor, 0.2f);
        });
    }
}
