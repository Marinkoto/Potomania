using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] bool canPierce;
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
        }
    }
}
