using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] bool canPierce;
    [SerializeField] bool canBounce;
    [SerializeField] LayerMask enemies;
    [SerializeField] float bounceForce = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(PlayerStats.Instance.damage);
            }
            if (!canPierce)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject, 4f);
            }
            if (canBounce)
            {
                BounceToAnotherEnemy();
                enemyHealth.TakeDamage(PlayerStats.Instance.damage);
            }
        }
    }

    void BounceToAnotherEnemy()
    {
        Vector2 direction = rb.velocity.normalized;

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, 4f, direction, Mathf.Infinity, enemies);

        foreach (RaycastHit2D hit in hits)
        {
            if(hit.collider.gameObject.TryGetComponent(out EnemyHealth enemy))
            {
                Vector2 hitDirection = (hit.collider.transform.position - transform.position).normalized;
                rb.velocity = hitDirection * bounceForce;
                break;
                
            }
        }
        Destroy(gameObject, 1f);
    }
}
