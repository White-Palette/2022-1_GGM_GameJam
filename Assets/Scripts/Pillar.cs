using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour, IPoolable
{
    public Pillar leftPillar;
    public Pillar rightPillar;

    public int index = 0;

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
