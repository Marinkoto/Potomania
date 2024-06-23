using Assets.Scripts.Misc;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [Header("Parameters")]
    public static int currentIndex;
    [Header("Components")]
    [SerializeField] List<Button> levelButtons;

    private string savePath;

    private void Start()
    {
        savePath = Application.persistentDataPath + "/levels";
        SetUpButtons();
        currentIndex = 0;
        LoadData();
    }
    private void OnDisable()
    {
        SaveData();
    }
    private void SetUpButtons()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            int index = i;
            levelButtons[i].onClick.AddListener(() => ReturnIndexOfButton(index));
        }
    }
    private void ReturnIndexOfButton(int index)
    {
        currentIndex = index;
    }
    private void SaveData()
    {
        GunManager.instance.SaveInventory(savePath);
    }
    private void LoadData()
    {
        if (File.Exists(savePath))
        {
            GunManager.instance.LoadInventory(savePath);
        }
        else
        {
            UnlockLevel(levelButtons[0].name);
        }
        if (SkillManager.instance.statsHolder.health >= 1.5 && SkillManager.instance.statsHolder.health <= 1.99)
        {
            UnlockLevel(levelButtons[1].name);
        }
        else if (SkillManager.instance.statsHolder.health >= 2)
        {
            UnlockLevel(levelButtons[2].name);
        }
        SaveData();
    }
    public void UnlockLevel(string name)
    {
        switch (name)
        {
            case "Tundra":
                levelButtons[1].interactable = GunManager.instance.inventory.unlockedLevels.Contains(name);
                if (levelButtons[1].interactable)
                {
                    levelButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Tundra";
                    GunManager.instance.inventory.unlockedLevels.Add(name);
                    SaveData();
                }
                else
                {
                    levelButtons[1].GetComponentInChildren<TextMeshProUGUI>().text = "Reach a HP stat of 1.5 to unlock.";
                }
                break;
            case "Desert":
                levelButtons[2].interactable = GunManager.instance.inventory.unlockedLevels.Contains(name);
                if (levelButtons[2].interactable)
                {
                    levelButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = "Desert";
                    GunManager.instance.inventory.unlockedLevels.Add(name);
                    SaveData();
                }
                else
                {
                    levelButtons[2].GetComponentInChildren<TextMeshProUGUI>().text = "Reach a HP stat of 2 to unlock.";
                }

                break;
            default:
                break;
        }
        SaveData();
    }
}

