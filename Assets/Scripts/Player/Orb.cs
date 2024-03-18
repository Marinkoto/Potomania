using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private LayerMask enemyLayer;
    [Header("Components")]
    [SerializeField] private Transform target; 

    void FixedUpdate()
    {
        RotateAroundTarget();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (enemyLayer == (enemyLayer | (1 << other.gameObject.layer)))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(PlayerStats.Instance.abilityDamage);
            }
        }
    }
    private void RotateAroundTarget()
    {
        transform.RotateAround(target.position, Vector3.forward, rotationSpeed * Time.deltaTime);
    }

}
