using CodeMonkey.MonoBehaviours;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    [Header("Components")]
    [SerializeField] AbilityManager abilityManager;
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
    [SerializeField] public int maxHealth;
    [SerializeField] public int playerHealth;
    [Header("Misc")]
    [SerializeField] public int bulletAmount;
    [Header("Experience System")]
    [SerializeField] int currentExp;
    [SerializeField] public int maxExp;
    [SerializeField] int level;
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
        level = 1;
    }
    private void OnEnable()
    {
        ExperienceManager.instance.OnExperienceChange += HandleExpChange;
        Camera.main.GetComponent<CameraFollowSetup>().followTransform = transform;
    }
    private void OnDisable()
    {
        ExperienceManager.instance.OnExperienceChange -= HandleExpChange;
    }
    private void HandleExpChange(int newExp)
    {
        currentExp += newExp;
    }
    private void Update()
    {
        ExperienceManager.instance.SetExperienceBar(currentExp, maxExp, level);
        if (currentExp >= maxExp)
        {
            LevelUp();
        }
    }
    private void LevelUp()
    {
        level++;
        damage += 0.15f;
        abilityDamage += 0.2f;
        abilityManager.UpdateUpgradePopupUI();
        if (level >= 5)
        {
            maxExp = Mathf.RoundToInt(maxExp * 1.5f);
        }
        else
        {
            maxExp = Mathf.RoundToInt(maxExp * 1.75f);

        }
        currentExp = 0;
    }
}
