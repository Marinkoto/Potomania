using Assets.Scripts.Misc;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Guns : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Button[] gunButtons;
    private string filePath;
    private void Start()
    {
        LoadFromInventory();
        filePath = Application.persistentDataPath + "/playerInventory.json"; ;
    }
    private void LoadFromInventory()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            PlayerInventory inventory = JsonUtility.FromJson<PlayerInventory>(json);

            foreach (string gun in inventory.purchasedGuns)
            {
                switch (gun)
                {
                    case "AK-47":
                        gunButtons[0].interactable = !gun.Equals("AK-47");
                        break;
                    case "Pistol":
                        gunButtons[1].interactable = !gun.Equals("Pistol");
                        break;
                    case "Shotgun":
                        gunButtons[2].interactable = !gun.Equals("Shotgun");
                        break;
                    case "Shuriken":
                        gunButtons[3].interactable = !gun.Equals("Shuriken");
                        break;
                    default:
                        Debug.Log("No valid gun purchased");
                        break;
                }
            }
        }
    }
}
