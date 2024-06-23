using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float moveSpeed;
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;

    private Vector2 moveDirection;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        GetInputs();
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void GetInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        moveDirection = new Vector2 (moveX, 0).normalized;
    }
    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y + 3 * moveSpeed);
    }
}
