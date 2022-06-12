using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Vector2 height;
    [SerializeField] Vector2 width;
    [SerializeField] Vector2 entireWidth;
    [SerializeField] Vector2 entireHeight;
    [SerializeField] float moveSpeed;

    private bool isMoving = false;

    public Filter ballon;

    private Rigidbody2D rb;

    private Touch touch;
    private Vector3 defualtPosition;

    private Coroutine shot;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defualtPosition = transform.position;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && !isMoving)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touch.position), Vector2.zero);
                if (hit.collider != null) { StartCoroutine(MoveWhileTouchEnded()); }
            }
            else if(touch.phase == TouchPhase.Ended)
            {
                shot = StartCoroutine(ShotBall());
            }
        }
    }
    
    private void moveToTouchPosition()
    {
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
        touchPos.z = 0;
        touchPos = new Vector2(Mathf.Clamp(touchPos.x, width.x, width.y), Mathf.Clamp(touchPos.y, height.x, height.y));
        rb.position = Vector2.Lerp(transform.position ,touchPos, 0.2f);
    }
    
    private IEnumerator ShotBall()
    {
        ChangeLayer("Default");
        float w_length = entireWidth.y - entireWidth.x;
        float h_length = entireHeight.y - entireHeight.x;
        isMoving = true;
        Vector2 permittedArea = new Vector2(width.y - width.x, height.y - height.x);
        float speed = moveSpeed;
        Vector3 current = transform.position;
        Vector3 destination = - new Vector2(w_length * current.x / permittedArea.x, h_length * current.y / permittedArea.y);
        while (Vector3.Distance(transform.position, destination) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
            speed -= Time.deltaTime;
            yield return null;
        }
        StartCoroutine(BackToDefaultPosition());
    }
    private IEnumerator BackToDefaultPosition()
    {
        float speed = moveSpeed;
        while (Vector3.Distance(transform.position, defualtPosition) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, defualtPosition, Time.deltaTime * speed);
            speed -= Time.deltaTime;
            yield return null;
        }
        isMoving = false;
        ChangeLayer("Untouchable");
    }
    public void OnHitBallon()
    {
        if(shot != null)
        {
            StopCoroutine(shot);
            StartCoroutine(BackToDefaultPosition());
        } 
    }
    IEnumerator MoveWhileTouchEnded()
    {
        while (touch.phase != TouchPhase.Ended)
        {
            moveToTouchPosition();
            yield return null;
        }
    }
    private void ChangeLayer(string layerName)
    {
        gameObject.layer = LayerMask.NameToLayer(layerName);
    }
    private void OnDrawGizmos()
     {
        Gizmos.DrawSphere(new Vector2(0, height.y), 0.3f);
        Gizmos.DrawSphere(new Vector2(0, height.x), 0.3f);
        Gizmos.DrawSphere(new Vector2(width.x, 0), 0.3f);
        Gizmos.DrawSphere(new Vector2(width.y, 0), 0.3f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(new Vector2(0, entireHeight.y), 0.3f);
        Gizmos.DrawSphere(new Vector2(0, entireHeight.x), 0.3f);
        Gizmos.DrawSphere(new Vector2(entireWidth.x, 0), 0.3f);
        Gizmos.DrawSphere(new Vector2(entireWidth.y, 0), 0.3f);
     }
}
