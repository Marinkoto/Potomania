using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRepulsion : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float repulsionForce = 10f; 
    [SerializeField] private float repulsionRange = 2f;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] Component enemyType;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CheckAndAdjustSpawnPosition();
    }

    void FixedUpdate()
    {
        ApplyRepulsionForce();
    }
    private void CheckAndAdjustSpawnPosition()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f); 
        foreach (Collider2D collider in colliders)
        {
            if (collider.gameObject != gameObject && collider.CompareTag("Enemy")) 
            {
                Vector3 spawnDirection = (collider.transform.position - transform.position).normalized;
                transform.position += spawnDirection * 0.5f; 
            }
        }
    }
    public void ApplyRepulsionForce()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, repulsionRange, enemyLayer); 

        foreach (Collider2D enemyCollider in nearbyEnemies)
        {
            if (enemyCollider.gameObject != gameObject) 
            {
                Vector2 repulsionDirection = (transform.position - enemyCollider.transform.position).normalized; 
                rb.AddForce(repulsionDirection * repulsionForce); 
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, repulsionRange);
    }
}
