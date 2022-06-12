using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObjectPooler))]
public class FlowerSpwaner : MonoBehaviour
{
    private Coroutine growFlower;
    private Animator an;
    private ObjectPooler pool;
    public float growTime;

    public Filter somarFilter;
    public Action OnFlowerRiped;

    [SerializeField] PoolObject flowerPrefab;
    [SerializeField] float waitTime;
    private void Start()
    {
        an = GetComponent<Animator>();
        pool = GetComponent<ObjectPooler>();
        growFlower = StartCoroutine(GrowFlower());

    }
    private IEnumerator GrowFlower()
    {
        yield return new WaitForSeconds(waitTime);
        an.SetBool("grow", true);
        yield return new WaitForSeconds(growTime);
        StartCoroutine(GrowFlower());
    }
    private void PopTheFlower()
    {
        var flower = pool.Spawn(flowerPrefab,transform.position, Quaternion.identity, transform);
    }
    public void ResetGrowTrigger()
    {
        an.SetBool("grow", false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Validate(somarFilter))
        {   
            an.SetTrigger("somar");
            StopCoroutine(growFlower);
            StartCoroutine(GrowFlower());
        }
    }
}