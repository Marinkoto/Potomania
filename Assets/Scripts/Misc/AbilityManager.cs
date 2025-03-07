using Cinemachine;
using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class AbilityManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject upgradePanel;
    [SerializeField] ParticleSystem particleEffect;
    [SerializeField] Image[] icons;
    [SerializeField] Button[] abilityButtons;

    [Header("Abilities")]
    [SerializeField] List<Ability> abilities;
    HashSet<Ability> chosenAbilities = new();

    private void Start()
    {
        upgradePanel.SetActive(false);

        for (int i = 0; i < abilityButtons.Length; i++)
        {
            int index = i;
            abilityButtons[i].onClick.AddListener(() => UpgradeAbility(index));
        }

        SetStartDescription();
        ShuffleAbilities();
    }

    private void UpgradeAbility(int abilityIndex)
    {
        Ability ability = abilities[abilityIndex];

        if (ability.CanUpgrade())
        {
            ability.Upgrade();
            UpdateDescription(ability, ability.currentLevel + 1);
            particleEffect.gameObject.SetActive(false);
        }
        else
        {
            UpdateUpgradePopupUI();
            particleEffect.gameObject.SetActive(false);
        }
    }

    private void SetStartDescription()
    {
        foreach (var ability in abilities)
        {
            ability.currentLevel = 0;
            SetDescription(ability);
        }
    }

    private void SetDescription(Ability ability)
    {
        switch (ability.type)
        {
            case Ability.AbilityType.Molotov:
                ability.description = "Player creates an explosive cocktail of beverage and throws it at a random position around himself. Burns enemies to ashes.";
                break;
            case Ability.AbilityType.Burp:
                ability.description = "Player gets dizzy and pukes a toxic beverage waste and enemies hit will get dissolved.";
                break;
            case Ability.AbilityType.Knife:
                ability.description = "Handy knives for close range fights, helps player with crowds of enemies and are a good thing to have in your armory.";
                break;
            case Ability.AbilityType.Orb:
                ability.description = "These orbs grant the player with safety with a wide range of fresh spirits. Also makes the player master of spirits.";
                break;
            case Ability.AbilityType.Shield:
                ability.description = "One time upgrade! Saves you from enemy hits no matter their strength.";
                break;
            case Ability.AbilityType.Bottle:
                ability.description = "These bottles fly at their own will the direction they are thrown is random and will not follow any command.";
                break;
            case Ability.AbilityType.Rock:
                ability.description = "Player starts to throw rocks at enemies, it detects enemies automatically so you don't have to worry about the aim.";
                break;
            case Ability.AbilityType.Ring:
                ability.description = "Player picks up bottles from a longer distance.";
                break;
            case Ability.AbilityType.Boots:
                ability.description = "Makes the player move faster because that's what good shoes do.";
                break;
            case Ability.AbilityType.QuickHands:
                ability.description = "The hands of the player become faster at shooting.";
                break;
            default:
                break;
        }
    }

    public void UpdateUpgradePopupUI()
    {
        upgradePanel.SetActive(true);
        particleEffect.gameObject.SetActive(true);
        ShuffleAbilities();
        Camera.main.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = false;

        for (int i = 0; i < abilityButtons.Length; i++)
        {
            if (abilities[i].CanUpgrade())
            {
                icons[i].sprite = abilities[i].icon;
                abilityButtons[i].gameObject.SetActive(true);
                TextMeshProUGUI abilityNameText = abilityButtons[i].GetComponentInChildren<TextMeshProUGUI>();

                if (abilityNameText != null)
                {
                    abilityNameText.text = abilities[i].abilityName;
                }

                TextMeshProUGUI[] textComponents = abilityButtons[i].GetComponentsInChildren<TextMeshProUGUI>(true);

                if (textComponents.Length > 1)
                {
                    TextMeshProUGUI descriptionText = textComponents[1];

                    if (descriptionText != null)
                    {
                        descriptionText.text = abilities[i].description;
                    }
                }
                chosenAbilities.Add(abilities[i]);
            }
            else
            {
                Ability replacementAbility = FindReplacementAbility(chosenAbilities);

                if (replacementAbility != null)
                {
                    abilities[i] = replacementAbility;
                    SetDescription(abilities[i]);
                    chosenAbilities.Add(replacementAbility);
                }
                break;
            }
        }
    }

    private Ability FindReplacementAbility(HashSet<Ability> chosenAbilities)
    {
        List<Ability> replacementAbility = new List<Ability>();

        for (int i = 0; i < abilities.Count; i++)
        {
            if (abilities[i].currentLevel == 0 && !chosenAbilities.Contains(abilities[i]))
            {
                replacementAbility.Add(abilities[i]);
                return replacementAbility[UnityEngine.Random.Range(0, replacementAbility.Count)];
            }
        }
        return null;
    }

    private void UpdateDescription(Ability ability, int newLevel)
    {
        switch (ability.type)
        {
            case Ability.AbilityType.Orb:
                if (newLevel % 2 == 0 && newLevel != 6)
                {
                    ability.description = $"Activates one more orb!!\nLevel: {ability.currentLevel} +1 / {ability.maxLevel}";
                }
                else if (newLevel % 2 == 1)
                {
                    ability.description = $"Increases rotation speed by 12%!\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                }
                else if (newLevel == 6)
                {
                    ability.description = $"Evolutionary Stage: The orbs spin in a bigger radius also becoming bigger!\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                }
                break;
            case Ability.AbilityType.Molotov:
                if (newLevel % 2 == 0)
                {
                    ability.description = $"Decreases cooldown by 1 second!\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                }
                else if (newLevel % 2 == 1)
                {
                    ability.description = $"Increases radius of throw by 0.5 meters!\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                }
                break;
            case Ability.AbilityType.Knife:
                if (newLevel == 3)
                {
                    ability.description = $"Activates one more Knife!\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                }
                else if (newLevel % 2 == 0)
                {
                    ability.description = $"Increases damage area by 5%\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                }
                else if (newLevel % 2 == 1 && newLevel != 5)
                {
                    ability.description = $"Increases damage area by 5%.\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                }
                else if (newLevel == 5)
                {
                    ability.description = $"Evolutionary Stage: The Knives start to spin around the player!\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}1";
                }
                break;
            case Ability.AbilityType.Rock:
                if (newLevel % 2 == 0)
                {
                    ability.description = $"Decreases shoot delay by 0.05 seconds!\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                }
                else if (newLevel % 2 == 1)
                {
                    ability.description = $"Increases damage area by 5%.\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                }
                break;
            case Ability.AbilityType.Bottle:
                if (newLevel % 2 == 0)
                {
                    ability.description = $"Decreases shoot delay by 0.1 seconds!\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                }
                else if (newLevel % 2 == 1 && newLevel != 5)
                {
                    ability.description = $"Increases bottle travel speed by 10%!\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                }
                else if (newLevel == 5)
                {
                    ability.description = $"Evolutionary Stage: Makes the bottles 2 times bigger!\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                }
                break;
            case Ability.AbilityType.Burp:
                if (newLevel % 2 == 0)
                {
                    ability.description = $"Decreases cooldown by 2 seconds.\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                }
                else if (newLevel % 2 == 1 && newLevel != 7)
                {
                    ability.description = $"Increases damage area by 5%.\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                }
                else if (newLevel == 7)
                {
                    ability.description = $"Evolutionary Stage: Activates 3 more Puke Points!\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                }
                break;
            case Ability.AbilityType.Ring:
                ability.description = $"Increases player pick up range by 1 meter.\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                break;
            case Ability.AbilityType.Boots:
                ability.description = $"Increases base move speed by 10%.\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                break;
            case Ability.AbilityType.QuickHands:
                ability.description = $"Decreases fire rate by 8%.\nLevel:{ability.currentLevel} +1 / {ability.maxLevel}";
                break;
            default:
                break;
        }
    }

    private void ShuffleAbilities()
    {
        System.Random rng = new System.Random();

        for (int i = abilities.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            Ability temp = abilities[i];
            abilities[i] = abilities[j];
            if (temp.CanUpgrade())
            {
                abilities[j] = temp;
            }
            else
            {
                abilities.Remove(abilities[j]);
            }
        }
    }


}
