using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour, IPoolable
{
    [SerializeField] Pillar leftPillar;
    [SerializeField] Pillar rightPillar;

    float offset = 3f;

    bool isInitUpTower = false;

    public void Initialize()
    {
        TowerManager.Instance.RegisterTower(gameObject);
        StartCoroutine(InitUpTowerCoroutine());
    }

    public void InitUpTower()
    {
        StartCoroutine(InitUpTowerCoroutine());
    }

    private IEnumerator InitUpTowerCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        if (Vector2.Distance(Camera.main.transform.position, transform.position) > 8f)
        {
            yield break;
        }

        if (isInitUpTower)
        {
            yield break;
        }

        isInitUpTower = true;

        int random = UnityEngine.Random.Range(0, 3);

        Pillar leftTemp = null;
        Pillar rightTemp = null;

        switch (random)
        {
            case 0:
                leftTemp = PoolManager<Pillar>.Get(TowerManager.Instance.transform);
                break;
            case 1:
                rightTemp = PoolManager<Pillar>.Get(TowerManager.Instance.transform);
                break;
            case 2:
                leftTemp = PoolManager<Pillar>.Get(TowerManager.Instance.transform);
                rightTemp = PoolManager<Pillar>.Get(TowerManager.Instance.transform);
                break;
            default:
                break;
        }

        if (leftTemp != null)
        {
            leftTemp.transform.position = transform.position + new Vector3(-offset, offset, 0);
        }

        if (rightTemp != null)
        {
            rightTemp.transform.position = transform.position + new Vector3(offset, offset, 0);
        }

        leftPillar = leftTemp;
        rightPillar = rightTemp;
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
