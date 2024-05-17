using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float detectionRadius = 5f;
    [SerializeField] float attackRange = 1.5f;
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float attackCooldown = 2f;
    [SerializeField] int attackDamage = 1;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] bool canAttack = true;
    [Header("Components")]
    [SerializeField] SpriteRenderer sr;

    private Transform player;
    void Start()
    {
        FindPlayer();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (player == null)
            return;

        CheckPlayerDistance();
    }

    private void FixedUpdate()
    {
        LookAtPlayer();
    }

    public void LookAtPlayer()
    {
        if (transform.position.x > player.position.x )
        {
            sr.flipX = false;
        }
        else if (transform.position.x < player.position.x)
        {
            sr.flipX = true;
        }
    }

    void FindPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void CheckPlayerDistance()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            HandleAttack(distanceToPlayer);
        }
    }

    void HandleAttack(float distanceToPlayer)
    {
        if (distanceToPlayer > attackRange)
        {
            MoveTowardsPlayer();
        }
        else if (canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    IEnumerator Attack()
    {
        canAttack = false;

        PerformAttack();

        yield return new WaitForSeconds(attackCooldown);

        canAttack = true;
    }

    void PerformAttack()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

        foreach (Collider2D collider in hitColliders)
        {
            if (collider.CompareTag("Player"))
            {
                PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                }
            }
        }
    }
}
