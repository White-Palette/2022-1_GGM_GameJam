using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour, IPoolable
{
    [SerializeField] Pillar leftPillar;
    [SerializeField] Pillar rightPillar;

    public void Initialize()
    {

    }

    public void MoveLeft()
    {
        if (leftPillar != null)
        {
            PlayerController.Instance.MoveToPillar(leftPillar);
        }
    }

    public void MoveRight()
    {
        if (rightPillar != null)
        {
            PlayerController.Instance.MoveToPillar(rightPillar);
        }
    }
}
