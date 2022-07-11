using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] Pillar currentPillar;

    private bool isMoving = false;

    private void Update()
    {
        if (!isMoving)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentPillar.MoveLeft();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentPillar.MoveRight();
            }
        }
    }

    public void MoveToPillar(Pillar pillar)
    {
        isMoving = true;
        currentPillar = pillar;
        TowerManager.Instance.UpdateTower();
        transform.DOJump(pillar.transform.position + Vector3.up * 1.7f, 2f, 1, 0.5f).SetEase(Ease.InOutQuad).OnComplete(() =>
        {
            isMoving = false;
        });
    }
}
