using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPillar : Pillar
{
    public override void TowerEvent()
    {
        PlayerController.Instance.Dead();
    }

    public override void Generate()
    {
        
    }
}
