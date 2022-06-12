using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Somar : PoolObject
{
    private Rigidbody2D rb;
    public Filter flower;
    [SerializeField] private float speed;
    public override void OnSpawned()
    {        
        base.OnSpawned();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
    }
    public void ChangeDirection()
    {
        rb.velocity = new Vector2(-speed, 0);
        transform.localScale = new Vector3(-1, 1, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Validate(flower))
        {
            ChangeDirection();
        }
    }
}
