using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotateAbility : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] public float rotationSpeed = 30f;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] public float abilityDistance = 5f;
    [SerializeField] bool inState;
    [Header("Components")]
    [SerializeField] Transform target;

    Transform otherAbility;

    private void OnEnable()
    {
        if (!inState)
        {
            FindOtherAbility();

            if (target != null && otherAbility != null)
            {
                float angleBetweenOrbs = Mathf.Atan2(otherAbility.position.y - target.position.y, otherAbility.position.x - target.position.x);

                for (int i = 0; i < 4; i++)
                {
                    float angle = angleBetweenOrbs + Mathf.PI / 2 * i;

                    Vector3 abilityPosition = target.position + new Vector3(Mathf.Cos(angle) * (abilityDistance + 2f), Mathf.Sin(angle) * (abilityDistance + 2f), 0);
                    transform.position = abilityPosition;
                }
            }
            else
            {
                PositionAbilitiesAroundPlayer();
            }
        }
    }

    private void FindOtherAbility()
    {
        RotateAbility[] abilities = FindObjectsOfType<RotateAbility>();

        foreach (RotateAbility ability in abilities)
        {
            if (ability != this)
            {
                otherAbility = ability.transform;
                break;
            }
        }
    }
    private void Start()
    {
        target = PlayerStats.Instance.transform;
        OnEnable();
    }
    void PositionAbilitiesAroundPlayer()
    {
        for (int i = 0; i < 4; i++)
        {
            float angle = Mathf.PI / 2 * i;
            Vector3 abilityPosition = target.position + new Vector3(Mathf.Cos(angle) * (abilityDistance + 2f), Mathf.Sin(angle) * (abilityDistance + 2f), 0);
            transform.position = abilityPosition;
        }
    }

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
