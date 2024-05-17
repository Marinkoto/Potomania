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
        [SerializeField] PlayerInventory inventory;
        private string filePath;
        private void OnDisable()
        {
            SaveInventory();
        }

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
        }
        private void Start()
        {
            SetButtons();
            filePath = Application.persistentDataPath + "/playerInventory.json";
            LoadInventory();
        }
        private void SetButtons()
        {
            gunButtons[0].onClick.AddListener(() => PurchaseGun("AK-47"));
            gunButtons[1].onClick.AddListener(() => PurchaseGun("Pistol"));
            gunButtons[2].onClick.AddListener(() => PurchaseGun("Shotgun"));
            gunButtons[3].onClick.AddListener(() => PurchaseGun("Shuriken"));
        }
        public void PurchaseGun(string gunName)
        {
            if (!inventory.purchasedGuns.Contains(gunName))
            {
                inventory.purchasedGuns.Add(gunName);
                SaveInventory();
            }
            SetButtonStates();
        }
        private void SaveInventory()
        {
            string json = JsonUtility.ToJson(inventory);
            File.WriteAllText(filePath, json);
        }
        public void LoadInventory()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
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

    }
}