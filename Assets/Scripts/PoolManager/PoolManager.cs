using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PoolManager<T> where T : MonoBehaviour, IPoolable
{
    private static GameObject _prefab = null;
    private static Dictionary<GameObject, bool> _pooledDict = new Dictionary<GameObject, bool>();
    private static Queue<GameObject> _objectQueue = new Queue<GameObject>();
    private static Transform _poolStore = null;

    public static int Count => _objectQueue.Count;
    public static int TotalCount => _pooledDict.Count;

    static PoolManager()
    {
        Debug.Log($"PoolManager[{typeof(T).Name}]: Initialize");
        SceneManager.activeSceneChanged += ActiveSceneChanged;
        _prefab = Resources.Load<GameObject>("Prefabs/" + typeof(T).Name);
    }

    private static void ActiveSceneChanged(Scene prev, Scene scene)
    {
        _poolStore = GameObject.Find("PoolStore").transform;
        if (_poolStore == null)
        {
            GameObject gameObject = new GameObject("PoolStore");
            _poolStore = gameObject.transform;
        }
        _pooledDict.Clear();
        _objectQueue.Clear();
    }

    public static T Get(Transform parant)
    {
        T pool = null;
        if (_objectQueue.Count > 0)
        {
            pool = _objectQueue.Dequeue().GetComponent<T>();
            pool.transform.SetParent(parant);
            pool.transform.localPosition = Vector3.zero;
        }
        else
        {
            GameObject gameObject = GameObject.Instantiate(_prefab, parant);
            gameObject.name = typeof(T).Name + (TotalCount + 1);
            pool = gameObject.GetComponent<T>();
        }
        _pooledDict[pool.gameObject] = false;
        pool.gameObject.SetActive(true);
        pool.Initialize();
        return pool;
    }

    public static void Release(T pool)
    {
        bool isPooled = false;
        if (_pooledDict.TryGetValue(pool.gameObject, out isPooled))
        {
            if (isPooled)
            {
                Debug.LogError($"{pool.gameObject.name} is not valid object");
                return;
            }
            pool.gameObject.SetActive(false);
            _objectQueue.Enqueue(pool.gameObject);
        }
        else
        {
            Debug.LogError($"{pool.gameObject.name} is not object in pool");
        }
    }
}