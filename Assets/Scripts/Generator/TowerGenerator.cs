using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGenerator : MonoSingleton<TowerGenerator>
{
    public Pillar GenerateTower(Transform parent, Vector2 position, PillarType type)
    {
        Pillar pillar = null;

        switch (type)
        {
            case PillarType.None:
                pillar = null;
                break;
            case PillarType.Normal:
                pillar = PoolManager<NormalPillar>.Get(parent, position);
                break;
            case PillarType.Enemy:
                pillar = PoolManager<GuardianPillar>.Get(parent, position);
                break;
            case PillarType.Trap:
                pillar = PoolManager<TrapPillar>.Get(parent, position);
                break;
            default:
                pillar = null;
                break;
        }

        return pillar;
    }
}
