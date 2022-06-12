using System.Collections;
using UnityEngine;

public class Flower : PoolObject
{
    [SerializeField] private float speed;
    public Filter player;
    public override void Update()
    {
        base.Update();
        transform.position -= Vector3.down * Mathf.Abs(speed) * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Validate(player))
        {
            GameManager.instance.GainBullet();
            Destroy(this.gameObject);
        }
    }
}