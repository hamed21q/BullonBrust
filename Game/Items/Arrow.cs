using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public int speed;
    private Vector3 direction;
    private void Start()
    {
        StartCoroutine(MoveOut());
        float angle = transform.eulerAngles.z * Mathf.Deg2Rad;
        direction = new Vector3(
            Mathf.Cos(angle),
            Mathf.Sin(angle),
            0);
    }
    private IEnumerator MoveOut()
    {
        float startTime = Time.time;
        while (Time.time - startTime < 10)
        {
            transform.position += direction * Time.deltaTime * speed;
            yield return null;
        }
    }
}
