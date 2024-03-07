using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageApplier : MonoBehaviour
{
    [Header("Paremeters")]
    [SerializeField] int factor;
    [SerializeField] bool constantApplier;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyHealth enemy) && !constantApplier)
        {
            enemy.TakeDamage(PlayerStats.Instance.abilityDamage / factor);
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyHealth enemy) && constantApplier)
        {
            enemy.TakeDamage(PlayerStats.Instance.abilityDamage / factor);
        }
    }
}
