using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] public int cost;
    [SerializeField] public float upgradeValue;
    [SerializeField] public int id;
    [SerializeField]
    [TextArea] string nameOfSkill;
    [SerializeField]
    [TextArea] string description;
    [Header("Components")]
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI descriptionText;
    [SerializeField] Skill nextSkill;
    [SerializeField] SkillData data;
    [SerializeField] SkillManager skillManager;

    private Button skillButton;
    public enum Type
    {
        Health,
        Damage,
        MoveSpeed
    }
    [SerializeField] Type type;
    private void OnDisable()
    {
        SaveSkillData(this);
    }

    private IEnumerator Start()
    {
        LoadSkillData(this);
        skillButton = gameObject.GetComponent<Button>();
        skillButton.onClick.AddListener(UpgradeSkill);
        nameText = GetComponentInChildren<TextMeshProUGUI>();
        descriptionText = nameText.gameObject.transform.parent.GetChild(1).GetComponent<TextMeshProUGUI>();
        SetUI();
        SetUpgradeValue();
        yield return new WaitForSeconds(0.1f);
        SetUnlockedState(this);
    }
    public void SaveSkillData(Skill skill)
    {
        string json = JsonUtility.ToJson(skill.data);
        string skillSavePath = Path.Combine(Application.persistentDataPath, $"skill_{skill.id}");
        File.WriteAllText(skillSavePath, json);
        SkillManager.instance.SaveData();
    }

    public void LoadSkillData(Skill skill)
    {
        string skillSavePath = Path.Combine(Application.persistentDataPath, $"skill_{skill.id}");
        if (File.Exists(skillSavePath))
        {
            string jsonData = File.ReadAllText(skillSavePath);
            SkillData skillData = JsonUtility.FromJson<SkillData>(jsonData);
            skill.data.upgraded = skillData.upgraded;
            if (skill.data.isUnlockedByDefault)
            {
                skill.data.unlocked = true;
            }
            else
            {
                skill.data.unlocked = skillData.unlocked;
            }
        }
        else
        {
            skill.data.upgraded = false;
            skill.data.unlocked = false;
        }
        if (skill.data.isUnlockedByDefault)
        {
            skill.data.unlocked = true;
        }
    }
    public void UpgradeSkill()
    {
        if (!CurrencyManager.instance.HasEnoughCurrency(cost))
        {
            CurrencyManager.instance.SetMessage($"Not enough SP to purchase {nameOfSkill} skill");
            return;
        }
        CurrencyManager.instance.RemoveCurrency(cost);
        if (type == Type.Health)
        {
            SkillManager.instance.statsHolder.health += upgradeValue;
        }
        if (type == Type.Damage)
        {
            SkillManager.instance.statsHolder.damage += upgradeValue;
        }
        if (type == Type.MoveSpeed)
        {
            SkillManager.instance.statsHolder.moveSpeedInRange += upgradeValue;
            SkillManager.instance.statsHolder.moveSpeedOutRange += upgradeValue - 0.05f;
        }
        this.data.upgraded = true;
        if (nextSkill != null)
        {
            this.nextSkill.data.unlocked = true;
        }
        SetUnlockedState(this);
    }
    private void SetUI()
    {
        nameText.text = nameOfSkill;
        descriptionText.text = $"{description}\n{cost}SP";
    }
    private void SetUnlockedState(Skill skill)
    {
        skill.skillButton.interactable = data.unlocked;
        if (data.upgraded)
        {
            skill.skillButton.image.color = Color.green;
            skill.skillButton.enabled = false;
            nextSkill.skillButton.interactable = true;
        }
        SaveSkillData(skill);
    }
    private void SetUpgradeValue()
    {
        if (type == Type.Health)
        {
            upgradeValue = 0.1f;
        }
        if (type == Type.Damage)
        {
            upgradeValue = 1f;
        }
        if (type == Type.MoveSpeed)
        {
            upgradeValue = 0.055f;
        }
    }
}
[System.Serializable]
public struct SkillData
{
    [Header("Parameters")]
    [SerializeField] public bool unlocked;
    [SerializeField] public bool upgraded;
    [Header("Needed")]
    [SerializeField] public bool isUnlockedByDefault;
}
