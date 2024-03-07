using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    [Header("Movement")]
    [SerializeField] public float moveSpeedInRadius;
    [SerializeField] public float moveSpeedOutRadius;
    [SerializeField] public float moveSpeed;
    [Header("Shooting")]
    [SerializeField] public float fireRateInRadius;
    [SerializeField] public float fireRateOutRadius;
    [SerializeField] public float fireRate;
    [SerializeField] public float damage;
    [SerializeField] public float abilityDamage;
    [Header("Health System")]
    [SerializeField] public int playerHealth,maxHealth;
    [Header("Misc")]
    [SerializeField] public int bulletAmount;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
    }
}
