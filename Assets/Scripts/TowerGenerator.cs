using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerGenerator : MonoSingleton<TowerGenerator>
{
    public Pillar GenerateTower(Transform parent, Vector2 position)
    {
        var tower = PoolManager<Pillar>.Get(parent, position);
        return tower;
    }
}
