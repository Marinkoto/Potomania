using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Misc
{
    public class GunManager : MonoBehaviour
    {
        public static GunManager instance;
        [Header("Components")]
        [SerializeField] private Button[] gunButtons;
        [SerializeField] public PlayerInventory inventory;
        private string filePath;
        private void OnDisable()
        {
            SaveInventory(filePath);
        }
        private void OnEnable()
        {
            LoadInventory(filePath);
        }

        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            filePath = Application.persistentDataPath + "/guns";
            LoadInventory(filePath);
        }
        public void PurchaseGun(string gunName)
        {
            switch (gunName)
            {
                case "Pistol":
                    if (!inventory.purchasedGuns.Contains(gunName) && CurrencyManager.instance.HasEnoughCurrency(500))
                    {
                        inventory.purchasedGuns.Add(gunName);
                        SaveInventory(filePath);
                    }
                    SetButtonStates();
                    break;
                case "Shotgun": 
                    if (!inventory.purchasedGuns.Contains(gunName) && CurrencyManager.instance.HasEnoughCurrency(1000))
                    {
                        inventory.purchasedGuns.Add(gunName);
                        SaveInventory(filePath);
                    }
                    SetButtonStates();
                    break;
                case "Shuriken":
                    if (!inventory.purchasedGuns.Contains(gunName) && CurrencyManager.instance.HasEnoughCurrency(1000))
                    {
                        inventory.purchasedGuns.Add(gunName);
                        SaveInventory(filePath);
                    }
                    SetButtonStates();
                    break;
                default:
                    break;
            }
        }
        public void SaveInventory(string path)
        {
            string json = JsonUtility.ToJson(inventory);
            File.WriteAllText(path, json);
        }
        public void LoadInventory(string path)
        {
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                inventory = JsonUtility.FromJson<PlayerInventory>(json);
            }
            else
            {
                inventory = new PlayerInventory();
                inventory.purchasedGuns.Add("AK-47");
            }

            SetButtonStates();
        }
        private void SetButtonStates()
        {
            gunButtons[0].interactable = !inventory.purchasedGuns.Contains("AK-47");
            gunButtons[1].interactable = !inventory.purchasedGuns.Contains("Pistol");
            gunButtons[2].interactable = !inventory.purchasedGuns.Contains("Shotgun");
            gunButtons[3].interactable = !inventory.purchasedGuns.Contains("Shuriken");
            foreach (Button button in gunButtons)
            {
                if (!button.interactable)
                {
                    button.GetComponentInChildren<TextMeshProUGUI>().text = "Bought";
                }
            }
        }
        
    }
    [System.Serializable]
    public class PlayerInventory
    {
        public List<string> purchasedGuns = new List<string>();
        public List<string> unlockedLevels = new List<string>();
    }
}