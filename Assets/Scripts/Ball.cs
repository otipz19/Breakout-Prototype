using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float defaultSpeed = 30f;

    private Rigidbody2D rigid;
    private float speed;
    private int hits;
    private int hitsWall;
    private GameObject lastCollided;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        speed = defaultSpeed;
    }

    private void Start()
    {
        Invoke("SetVelocity", 1f);
    }

    private void SetVelocity()
    {
        Vector2 dir;
        if (rigid.velocity == Vector2.zero)
            dir = Quaternion.AngleAxis(45f, Vector3.back) * Vector2.down;
        else if (hitsWall == 4)
            dir = Quaternion.AngleAxis(135f, Vector3.back) * Vector2.down;
        else
            dir = rigid.velocity.normalized;
           
        rigid.velocity = dir * speed;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (lastCollided == null)
            lastCollided = collision.gameObject;

        if(lastCollided != collision.gameObject)
        {
            if (collision.gameObject.tag == "VerticalWall")
                hitsWall++;
            else
                hitsWall = 0;

            hits++;

            if (hits % 4 == 0)
                speed = defaultSpeed * 1.3f;
            if (hits % 8 == 0)
                speed = defaultSpeed * 1.6f;
            if (hits % 12 == 0)
                speed = defaultSpeed;

            if (Random.value < 0.5f)
                speed *= 1.2f;

            lastCollided = collision.gameObject;
        }

        SetVelocity();
    }
}
