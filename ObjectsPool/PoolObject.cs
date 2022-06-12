using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private bool isActive = true;
    private float deathTime = -1;

    private bool started = false;

    public bool active
    {
        get
        {
            return isActive;
        }
        set
        {
            SetActive(value);
        }
    }
    public virtual void Start()
    {
        if (!started)
            OnStart();
    }
    public virtual void OnStart()
    {
        started = true;
    }
    public virtual void OnSpawned()
    {
        deathTime = -1;
    }
    public virtual void FixedUpdate()
    {

    }
    public virtual void Update()
    {
        if (deathTime >= 0)
        {
            if (Time.time >= deathTime)
            {
                OnDestroyed();
            }
        }
    }

    public void Spawn(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
        if (!isActive)
            OnSpawned();
        SetActive(true);
    }
    public virtual void SetActive(bool b)
    {
        isActive = b;
        gameObject.SetActive(b);
    }
    public virtual void Destroy(float t = 0)
    {
        deathTime = t + Time.time;
        if (t == 0)
        {
            OnDestroyed();
        }
    }
    public virtual void OnDestroyed()
    {
        SetActive(false);
    }
}
