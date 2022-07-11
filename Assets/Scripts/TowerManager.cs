using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoSingleton<TowerManager>
{
    private GameObject _towerPrefabs = null;

    private List<GameObject> _towerList = new List<GameObject>();

    private void Awake()
    {
        _towerPrefabs = Resources.Load<GameObject>("Prefabs/TowerPrefab");
        CreateTowerTree(new Vector2(0, -2.175f));
        PlayerController.Instance.MoveToPillar(_towerList[0].GetComponent<Pillar>());
    }

    public void UpdateTower()
    {

    }

    public void CreateTowerTree(Vector2 position)
    {
        if (position.y > 5.5f)
        {
            return;
        }



        CreateTowerTree(position + new Vector2(0, 1.7f));
    }

    public void DestroyTower(GameObject tower)
    {
        _towerList.Remove(tower);
        Destroy(tower);
        // PoolManager<Pillar>.Release(tower.GetComponent<Pillar>());
    }
}
