using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [HideInInspector]
    static public Paddle S;

    [Header("Set in Inspector")]
    public float speed = 30f;

    private Rigidbody2D rigid;
    private float moveDir;

    private void Awake()
    {
        S = this;
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveDir = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Vector2 moveTo = transform.position;
        moveTo.x += moveDir * speed * Time.fixedDeltaTime;
        rigid.MovePosition(moveTo);
    }
}