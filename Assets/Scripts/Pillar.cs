using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour, IPoolable
{
    [SerializeField] Pillar leftPillar;
    [SerializeField] Pillar rightPillar;

    float offset = 3f;

    public void Initialize()
    {
        TowerManager.Instance.RegisterTower(gameObject);
        StartCoroutine(InitUpTower());
    }

    private IEnumerator InitUpTower()
    {
        yield return new WaitForSeconds(0.5f);
        if (Vector2.Distance(Camera.main.transform.position, transform.position) > 8f)
        {
            yield break;
        }

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
            leftPillar = leftTemp;
        }

        if (rightTemp != null)
        {
            rightTemp.transform.position = transform.position + new Vector3(offset, offset, 0);
            rightPillar = rightTemp;
        }
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
