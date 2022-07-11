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
        SceneManager.sceneUnloaded += SceneUnloaded;
        _prefab = Resources.Load<GameObject>("Prefabs/" + typeof(T).Name);
    }

    private static void SceneUnloaded(Scene scene)
    {
        var poolStore = GameObject.Find("PoolStore");
        if (poolStore != null)
        {
            _poolStore = poolStore.transform;
        }
        else
        {
            GameObject gameObject = new GameObject("PoolStore");
            _poolStore = gameObject.transform;
        }
        _pooledDict.Clear();
        _objectQueue.Clear();
    }

    public static T Get(Transform parent, Vector3 position = new Vector3())
    {
        T pool = null;
        if (_objectQueue.Count > 0)
        {
            pool = _objectQueue.Dequeue().GetComponent<T>();
            pool.transform.SetParent(parent);
            pool.transform.localPosition = Vector3.zero;
        }
        else
        {
            Debug.Log("Init " + typeof(T).Name);
            GameObject gameObject = GameObject.Instantiate(_prefab, parent);
            gameObject.name = $"{typeof(T).Name} {TotalCount + 1}";
            pool = gameObject.GetComponent<T>();
        }
        _pooledDict[pool.gameObject] = false;
        Debug.Log($"PoolManager[{typeof(T).Name}]: Get {pool.gameObject.name}");
        pool.gameObject.SetActive(true);
        pool.transform.position = position;
        pool.Initialize();
        return pool;
    }

    public static void Release(T pool, bool force = false)
    {
        bool isPooled = false;
        if (_pooledDict.TryGetValue(pool.gameObject, out isPooled) || force)
        {
            if (isPooled)
            {
                Debug.LogError($"{pool.gameObject.name} is not valid object");
                return;
            }
            _pooledDict[pool.gameObject] = true;
            pool.gameObject.SetActive(false);
            _objectQueue.Enqueue(pool.gameObject);
        }
        else
        {
            Debug.LogError($"{pool.gameObject.name} is not object in pool");
        }
    }
}