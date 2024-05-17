using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Ability", menuName = "Abilities/Ability")]
public class Ability : ScriptableObject
{
    [Header("Parameters and Components")]
    [SerializeField] public string abilityName;
    [TextArea]
    [SerializeField] public string description;
    [SerializeField] public int maxLevel = 5;
    [SerializeField] public int currentLevel;
    [SerializeField] public GameObject abilityBehaviour;
    [SerializeField] public Sprite icon;
    public enum AbilityType
    {
        Molotov,
        Shield,
        Knife,
        Orb,
        Rock,
        Bottle,
        Burp,
        Ring,
        Boots,
        QuickHands
    }
    [SerializeField] public AbilityType type;

    public void Upgrade()
    {
        if (CanUpgrade())
        {
            currentLevel++;
            switch (type)
            {
                case Ability.AbilityType.Orb:
                    abilityBehaviour = PlayerStats.Instance.gameObject;
                    RotateAbility[] orbs = abilityBehaviour.GetComponentsInChildren<RotateAbility>(true);
                    float rotationSpeed = orbs[0].rotationSpeed;
                    if(currentLevel == 1)
                    {
                        orbs[0].gameObject.SetActive(true);
                    }
                    else if (currentLevel % 2 == 0 && currentLevel != 6)
                    {
                        int activatedOrb = currentLevel % 4;
                        switch (activatedOrb)
                        {
                            case 2: 
                                orbs[1].gameObject.SetActive(true);
                                break;
                            case 0: 
                                orbs[2].gameObject.SetActive(true);
                                break;
                        }
                    }
                    else if (currentLevel % 2 == 1)
                    {
                        for (int i = 0; i < orbs.Length; i++)
                        {
                            if (orbs[i].gameObject.activeSelf)
                            {
                                orbs[i].rotationSpeed += 15;
                                rotationSpeed = orbs[i].rotationSpeed;
                            }
                        }
                    }
                    else if(currentLevel == 6)
                    {
                        foreach (var orb in orbs)
                        {
                            orb.gameObject.SetActive(true);
                            orb.transform.localScale = new Vector2(1.25f, 1.25f);
                        }
                    }
                    foreach (var orb in orbs)
                    {
                        orb.rotationSpeed = rotationSpeed;
                    }
                    break;
                case Ability.AbilityType.Molotov:
                    abilityBehaviour = PlayerStats.Instance.gameObject;
                    Molotov molotov = abilityBehaviour.GetComponent<Molotov>();
                    if (currentLevel == 1)
                    {
                        molotov.enabled = true;
                    }
                    if (currentLevel % 2 == 0)
                    {
                        molotov.cooldown -= 1;
                    }
                    else if (currentLevel % 2 == 1)
                    {
                        molotov.circleRadius += 0.5f;
                    }
                    break;
                case Ability.AbilityType.Knife:
                    abilityBehaviour = PlayerStats.Instance.gameObject;
                    DamageApplier[] knives = abilityBehaviour.GetComponentsInChildren<DamageApplier>(true);
                    if (currentLevel == 1)
                    {
                        knives[0].gameObject.SetActive(true);
                    }
                    if (currentLevel == 3)
                    {
                        for (int i = 0; i <= 1; i++)
                        {
                            if (!knives[i].gameObject.activeSelf)
                            {
                                knives[i].gameObject.SetActive(true);
                            }
                        }
                    }
                    else if (currentLevel % 2 == 0)
                    {
                        for (int i = 0; i < knives.Length; i++)
                        {
                            if (knives[i].gameObject.activeSelf)
                            {
                                knives[i].gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.25f + .02f, 1);
                            }
                        }
                    }
                    else if (currentLevel % 2 == 1 && currentLevel != 5)
                    {
                        for (int i = 0; i < knives.Length; i++)
                        {
                            if (knives[i].gameObject.activeSelf)
                            {
                                knives[i].gameObject.GetComponent<BoxCollider2D>().size = new Vector2(0.25f + .02f, 1);
                            }
                        }
                    }
                    else if(currentLevel == 5)
                    {
                        foreach (var knife in knives)
                        {
                            knife.AddComponent<RotateAbility>();
                        }
                    }
                    break;
                case Ability.AbilityType.Rock:
                    abilityBehaviour = PlayerStats.Instance.gameObject;
                    BulletInstantiater homingBullet = abilityBehaviour.GetComponent<BulletInstantiater>();
                    if (currentLevel == 1)
                    {
                        homingBullet.enabled = true;
                    }
                    if (currentLevel % 2 == 0)
                    {
                        homingBullet.shootDelay -= 0.05f;
                    }
                    break;
                case Ability.AbilityType.Bottle:
                    abilityBehaviour = PlayerStats.Instance.gameObject;
                    Bottles bottle = abilityBehaviour.GetComponent<Bottles>();
                    if (currentLevel == 1)
                    {
                        bottle.gameObject.GetComponent<Bottles>().enabled = true;
                    }
                    if (currentLevel % 2 == 0)
                    {
                        bottle.shootDelay -= 0.1f;
                    }
                    else if (currentLevel % 2 == 1 && currentLevel != 5)
                    {
                        bottle.shootSpeed += 1f;
                    }
                    else if (currentLevel == 5)
                    {
                        bottle.bottlePrefab.transform.localScale = new Vector2(2, 2);
                    }
                    break;
                case Ability.AbilityType.Burp:
                    abilityBehaviour = PlayerStats.Instance.gameObject;
                    Burp burp = abilityBehaviour.GetComponentInChildren<Burp>();
                    if (currentLevel == 1)
                    {
                        burp.enabled = true;
                        burp.burpParticles[0].gameObject.SetActive(true);
                    }
                    if (currentLevel % 2 == 0 && currentLevel != 7)
                    {
                        burp.cooldown -= 2f;
                    }
                    else if (currentLevel == 7)
                    {
                        ParticleSystem[] burps = burp.GetComponent<Burp>().burpParticles;
                        foreach (ParticleSystem burpParticle in burps)
                        {
                            burpParticle.gameObject.SetActive(true);
                        }
                    }
                    break;
                case AbilityType.Shield:
                    abilityBehaviour = PlayerStats.Instance.gameObject;
                    ShieldTrigger shield = abilityBehaviour.GetComponent<ShieldTrigger>();
                    if (currentLevel == maxLevel)
                    {
                        shield.enabled = true;
                    }
                    break;
                case AbilityType.Ring:
                    PlayerStats.Instance.pickUpRange += 1;
                    break;
                case AbilityType.Boots:
                    PlayerStats.Instance.moveSpeedInRadius += 0.24f;
                    PlayerStats.Instance.moveSpeedOutRadius += 0.24f;
                    break;
                case AbilityType.QuickHands:
                    PlayerStats.Instance.fireRateInRadius -= 0.03f;
                    PlayerStats.Instance.fireRateOutRadius -= 0.05f;
                    break;
                default:
                    break;
            }
        }
    }

    public bool CanUpgrade()
    {
        return currentLevel < maxLevel;
    }
}