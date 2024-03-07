using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sr;
    Vector2 moveDirection;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        GatherInputs();
        Animate();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Animate()
    {
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Speed", moveDirection.magnitude);

        FlipSprite();
    }

    private void FlipSprite()
    {
        if (moveDirection.x > 0.1f)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    private void GatherInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * PlayerStats.Instance.moveSpeed, moveDirection.y * PlayerStats.Instance.moveSpeed);
    }
}
