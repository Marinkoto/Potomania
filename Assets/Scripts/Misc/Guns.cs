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
    private void Start()
    {
        LoadFromInventory();
    }
    private void LoadFromInventory()
    {
        PlayerInventory inventory = GunManager.instance.inventory;

        for (int i = 0; i < gunButtons.Length; i++)
        {
            gunButtons[i].interactable = false; 
        }

        foreach (var gun in inventory.purchasedGuns)
        {
            switch (gun)
            {
                case "AK-47":
                    gunButtons[0].interactable = true;
                    break;
                case "Pistol":
                    gunButtons[1].interactable = true;
                    break;
                case "Shotgun":
                    gunButtons[2].interactable = true;
                    break;
                case "Shuriken":
                    gunButtons[3].interactable = true;
                    break;
                default:
                    Debug.Log("No valid gun purchased");
                    break;
            }
        }
    }
}
