using System.Collections;
using UnityEngine;
public class PlayerCollisionDetector : MonoBehaviour
{
    public Filter somarFilter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.Validate(somarFilter))
        {
            collision.GetComponent<Somar>().ChangeDirection();
        }
    }
}