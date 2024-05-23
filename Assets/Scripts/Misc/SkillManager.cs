using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    [Header("References")]
    [SerializeField] public StatHolder statHolder;

    private string filePath;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath + "/stats");
        LoadData();
    }
    private void OnDisable()
    {
        SaveData();
    }
    public void SaveData()
    {
        string json = JsonUtility.ToJson(statHolder);
        File.WriteAllText(filePath, json);
    }
    public void LoadData()
    {
        if(File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            StatHolder loadedStat = JsonUtility.FromJson<StatHolder>(json);
            statHolder.health = loadedStat.health;
            statHolder.damage = loadedStat.damage;
            statHolder.moveSpeedOutRange = loadedStat.moveSpeedOutRange;
            statHolder.moveSpeedInRange = loadedStat.moveSpeedInRange;
        }
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
