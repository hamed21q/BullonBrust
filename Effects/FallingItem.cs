using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingItem : PoolObject
{
    public float fallSpeed = 1;
    public Filter player;
    public Filter ground;
    public PoolObject collectEffect;

    protected bool isUsed = false;
    protected GameManager game;

    protected ObjectPooler pool;

    public override void OnStart()
    {
        base.OnStart();
        game = GameManager.instance;
        pool = ObjectPooler.instance;
    }

    public override void OnSpawned()
    {
        base.OnSpawned();
        isUsed = false;
    }

    public override void Update()
    {
        base.Update();
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!isUsed && other.gameObject.Validate(player))
        {
            isUsed = true;
            game.GainScore();
            other.GetComponent<PlayerMovement>().OnHitBallon();
            if (collectEffect != null)
                pool.Spawn(collectEffect, transform.position, transform.rotation).Destroy(8);
            Destroy();
        }
        if (other.gameObject.Validate(ground))
        {
            game.OnBallonHitGround();
            Destroy(1);
        }
    }

}
