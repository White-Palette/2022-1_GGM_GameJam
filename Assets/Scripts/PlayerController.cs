using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoSingleton<PlayerController>
{
    [SerializeField] Pillar currentPillar;

    private void Update()
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

    public void MoveToPillar(Pillar pillar)
    {
        currentPillar = pillar;
        transform.position = pillar.transform.position + Vector3.up * 2.55f;
    }
}
