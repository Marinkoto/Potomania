using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    [Header("References")]
    [SerializeField] public StatHolder statsHolder;
    [Header("Components")]
    [SerializeField] TextMeshProUGUI statsText;

    private string filePath;
    void Awake()
    {
        if (instance != null && instance == this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }
    private void Update()
    {
        UpdateStatsUI();
    }
    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath + "/stats");
        LoadData();
        DontDestroyOnLoad(gameObject);
    }
    private void OnDisable()
    {
        SaveData();
    }
    public void SaveData()
    {
        string json = JsonUtility.ToJson(statsHolder);
        File.WriteAllText(filePath, json);
    }
    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            StatHolder loadedStat = JsonUtility.FromJson<StatHolder>(json);
            statsHolder.health = loadedStat.health;
            statsHolder.damage = loadedStat.damage;
            statsHolder.moveSpeedOutRange = loadedStat.moveSpeedOutRange;
            statsHolder.moveSpeedInRange = loadedStat.moveSpeedInRange;
        }
        else
        {
            statsHolder.health = 1;
            statsHolder.damage = 1;
            statsHolder.moveSpeedOutRange = 4;
            statsHolder.moveSpeedInRange = 6.5f;
        }
    }
    public void UpdateStatsUI()
    {
        statsText.text = "Stats\n" +
            $"Ability Damage:{statsHolder.damage / 2:f2}\n" +
            $"Normal Damage: {statsHolder.damage:f2}\n" +
            $"Health: {statsHolder.health:f2}\n" +
            $"In Range Move Speed: {statsHolder.moveSpeedInRange:f2}\n" +
            $"Out Range Move Speed: {statsHolder.moveSpeedOutRange:f2}";
    }
}
[System.Serializable]
public struct StatHolder
{
    [Header("Stat Holder")]
    [SerializeField] public float damage;
    [SerializeField] public float health;
    [SerializeField] public float moveSpeedInRange;
    [SerializeField] public float moveSpeedOutRange;
}
