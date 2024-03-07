using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float speed = 10f;

    private Transform target;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        FindClosestEnemy();
    }

    void FindClosestEnemy()
    {
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in allEnemies)
        {
            EnemyMelee behavior = enemy.GetComponent<EnemyMelee>();
            if (behavior != null && behavior.enabled)
            {
                float distanceToEnemy = Vector3.Distance(player.transform.position, enemy.transform.position);
                if (distanceToEnemy < shortestDistance)
                {
                    shortestDistance = distanceToEnemy;
                    nearestEnemy = enemy;
                }
            }
        }
        if (nearestEnemy != null)
        {
            target = nearestEnemy.transform;
        }
    }
    void FixedUpdate()
    {
        MoveTowardsTarget();
    }
    private void MoveTowardsTarget()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.fixedDeltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget(target);
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
    }

    void HitTarget(Transform enemyTransform)
    {
        EnemyHealth enemy = enemyTransform.GetComponent<EnemyHealth>();
        if (enemy != null)
        {
            enemy.TakeDamage(PlayerStats.Instance.abilityDamage);
        }
        Destroy(gameObject);
    }
}
