using System.Collections;
using UnityEngine;

public class SomarSpawner : MonoBehaviour
{
    public PoolObject prefab;
    public float waitTime;
    public float moveTime;

    private WaitForSeconds waiter;
    private WaitForSeconds movingTime;
    private ObjectPooler pooler;
    private void Start()
    {
        pooler = ObjectPooler.instance;
        waiter = new WaitForSeconds(waitTime);
        movingTime = new WaitForSeconds(moveTime);
        StartCoroutine(ActivateSomar());
    }
    private IEnumerator ActivateSomar()
    {
        yield return waiter;
        PoolObject obj = pooler.Spawn(prefab, transform.position, Quaternion.identity);
        Destroy(obj.gameObject, 20);
        yield return movingTime;
        StartCoroutine(ActivateSomar());
    }
}