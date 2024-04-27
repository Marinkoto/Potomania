using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageApplierParticle : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] ParticleSystem particles;

    List<ParticleCollisionEvent> colEvents = new List<ParticleCollisionEvent>();
    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }
    private void OnParticleCollision(GameObject other)
    {
        particles.GetCollisionEvents(other,colEvents);
        if(other.TryGetComponent(out EnemyHealth enemy))
        {
            enemy.TakeDamage(PlayerStats.Instance.abilityDamage / 2);
        }
    }
}
