using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoSingleton<TowerManager>
{
    private GameObject _towerPrefabs = null;

    public List<GameObject> _towerList = new List<GameObject>();

    private void Awake()
    {
        _towerPrefabs = Resources.Load<GameObject>("Prefabs/TowerPrefab");
        PoolManager<Pillar>.Get(transform);
        PlayerController.Instance.MoveToPillar(_towerList[0].GetComponent<Pillar>());
    }

    public void UpdateTower()
    {
        foreach (var tower in _towerList)
        {
            if (Vector2.Distance(Camera.main.transform.position, tower.transform.position) > 4f)
            {
                DestroyTower(tower);
            }
        }
    }

    public void RegisterTower(GameObject tower)
    {
        _towerList.Add(tower);
    }

    public void DestroyTower(GameObject tower)
    {
        _towerList.Remove(tower);
        Destroy(tower);
        PoolManager<Pillar>.Release(tower.GetComponent<Pillar>());
    }
}
