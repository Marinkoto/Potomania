using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static SkillManager;

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
        SkillData skillData = new SkillData();
        skillData.upgraded = skill.data.upgraded;
        skillData.unlocked = skill.data.unlocked;

        string json = JsonUtility.ToJson(skillData);
        string skillSavePath = Path.Combine(Application.persistentDataPath, $"skill_{skill.id}.json");
        File.WriteAllText(skillSavePath, json);
        SkillManager.instance.SaveData();
    }

    public void LoadSkillData(Skill skill)
    {
        string skillSavePath = Path.Combine(Application.persistentDataPath, $"skill_{skill.id}.json");
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
            return;
        }
        CurrencyManager.instance.RemoveCurrency(cost);
        if (type == Type.Health)
        {
            instance.statHolder.health += upgradeValue;
        }
        if (type == Type.Damage)
        {
            instance.statHolder.damage += upgradeValue;
        }
        if (type == Type.MoveSpeed)
        {
            instance.statHolder.moveSpeedInRange += upgradeValue;
            instance.statHolder.moveSpeedOutRange += upgradeValue - 0.05f;
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
public class SkillData
{
    [Header("Parameters")]
    [SerializeField] public bool unlocked = false;
    [SerializeField] public bool upgraded = false;
    [Header("Needed")]
    [SerializeField] public bool isUnlockedByDefault = false;
}
