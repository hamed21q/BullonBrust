using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ObjectPooler : MonoBehaviour
{
    private Dictionary<PoolObject, List<PoolObject>> poolDic = new Dictionary<PoolObject, List<PoolObject>>();

    public static ObjectPooler instance { get; private set; }

    public const string poolTag = "Pool";

    private void Awake()
    {
        if (tag == poolTag)
            instance = this;
    }

    public void AddPool(PoolObject prefab)
    {
        poolDic.Add(prefab, new List<PoolObject>());
    }

    public T Spawn<T>(T prefab) where T : PoolObject
    {
        return Spawn(prefab, prefab.transform.position, prefab.transform.rotation);
    }
    public T Spawn<T>(T prefab, Transform parent) where T : PoolObject
    {
        return Spawn(prefab, prefab.transform.position, prefab.transform.rotation, parent);
    }
    public T Spawn<T>(T prefab, Vector3 position, Quaternion rotation) where T : PoolObject
    {
        return Spawn(prefab, position, rotation, null);
    }
    public T Spawn<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent) where T : PoolObject
    {
        PoolObject obj = null;
        if (!poolDic.ContainsKey(prefab))
            AddPool(prefab);

        for (int i = 0; i < poolDic[prefab].Count; i++)
        {
            if (!poolDic[prefab][i].active)
            {
                obj = poolDic[prefab][i];
                break;
            }
        }
        if (obj == null)
        {
            obj = Ins(prefab, position, rotation, parent);
            poolDic[prefab].Add(obj);
            obj.OnStart();
        }
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.transform.SetParent(parent);
        obj.transform.SetAsLastSibling();
        obj.SetActive(true);

        obj.OnSpawned();
        return (T)obj;
    }

    public virtual PoolObject Ins(PoolObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        return Instantiate(prefab, position, rotation, parent);
    }
}
