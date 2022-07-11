using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoSingleton<TowerManager>
{
    public List<Pillar> _pillars = new List<Pillar>();

    public int _currentPillarIndex = 0;
    public int _bestPillarIndex = 0;
    public int _lowestPillarIndex = 0;

    public void InitUpTowers()
    {

    }

    public void CreateTower(Vector2 pos, Pillar leftDownPillar, Pillar rightDownPillar)
    {
        var pillar = PoolManager<Pillar>.Get(transform);
        pillar.transform.position = pos;
        pillar.rightPillar = leftDownPillar;
        pillar.leftPillar = rightDownPillar;
        pillar.index = _pillars.Count;
        _pillars.Add(pillar);
    }
}
