using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private static ObjectPool instance;
    private Dictionary<string,Queue<GameObject>> objectPool = new Dictionary<string,Queue<GameObject>>();
    private GameObject pool;
    public Dictionary<string,int> objectMaxNum = new Dictionary<string,int>();
    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance;
        }
    }
    public void Reset()
    {
        objectPool.Clear();
        pool = null;
    }

    public GameObject GetObject(GameObject prefab)
    {
        GameObject _object;
        if(!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)
        {
            _object = GameObject.Instantiate(prefab);
            PushObject(_object);
            if(pool == null)
            {
                pool = new GameObject("ObjectPool");
            }
            GameObject childPool = GameObject.Find(prefab.name + "Pool");
            if(childPool == null)
            {
                childPool = new GameObject(prefab.name + "Pool");
                childPool.transform.SetParent(pool.transform);
            }
            _object.transform.SetParent(childPool.transform);
        }
        _object = objectPool[prefab.name].Dequeue();
        _object.SetActive(true);
        return _object;
    }

    public void PushObject(GameObject _object)
    {
        string _name = _object.name.Replace("(Clone)", string.Empty);
        if (!objectPool.ContainsKey(_name))
        {
            objectPool[_name] = new Queue<GameObject>();
        }
        _object.SetActive(false);
        objectPool[_name].Enqueue(_object);
    }
}
