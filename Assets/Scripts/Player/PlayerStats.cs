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
    [SerializeField] public float maxHealth;
    [SerializeField] public float playerHealth;
    [Header("Misc")]
    [SerializeField] public int bulletAmount;
    [SerializeField] public PlayerType type;
    [Header("Experience System")]
    [SerializeField] int currentExp;
    [SerializeField] public int maxExp;
    [SerializeField] int level;
    [SerializeField] public int pickUpRange;
    public enum PlayerType
    {
        AK,
        Pistol,
        Shotgun,
        Shuriken
    }

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
    private void Start()
    {
        SetStats();
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
        damage += 0.05f;
        abilityDamage += 0.2f;
        AudioManager.instance.PlayLevelUPSFX(AudioManager.instance.levelUp);
        abilityManager.UpdateUpgradePopupUI();
        if (level >= 5)
        {
            maxExp = Mathf.RoundToInt(maxExp * 1.25f);
        }
        else
        {
            maxExp = Mathf.RoundToInt(maxExp * 1.75f);

        }
        currentExp = 0;
    }
    private void SetStats()
    {
        switch (type)
        {
            case PlayerType.AK:
                damage = SkillManager.instance.damage / 4;
                break;
            case PlayerType.Pistol:
                damage = SkillManager.instance.damage;
                break;
            case PlayerType.Shotgun:
                damage = SkillManager.instance.damage / 4;
                break;
            case PlayerType.Shuriken:
                damage = SkillManager.instance.damage / 3;
                break;
            default:
                break;
        }
        maxHealth = SkillManager.instance.health;
        moveSpeedInRadius = SkillManager.instance.moveSpeedInRange;
        moveSpeedOutRadius = SkillManager.instance.moveSpeedOutRange;
    }
}
