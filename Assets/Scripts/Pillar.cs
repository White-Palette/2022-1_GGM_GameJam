using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [SerializeField] Pillar leftPillar;
    [SerializeField] Pillar rightPillar;

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
