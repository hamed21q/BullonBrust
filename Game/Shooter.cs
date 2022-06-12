using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    private ObjectPooler pooler;
    public PoolObject prefab;
    private void Start()
    {
        pooler = ObjectPooler.instance;
    }
    public void Shoot()
    {

        PoolObject obj = pooler.Spawn(prefab, Vector3.zero, Quaternion.identity);
        //Destroy(obj.gameObject, 10);
    }
}
