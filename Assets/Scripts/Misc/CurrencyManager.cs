using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using Unity.VisualScripting;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    [Header("Parameters")]
    [SerializeField] public int currency;
    [SerializeField] string savePath;
    [Header("Components")]
    [SerializeField] CurrencyUI currencyUI;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Path.Combine(Application.persistentDataPath, "currency.json");
            LoadCurrency();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCurrency(int amount)
    {
        currency += amount;
        SaveCurrency();
    }
    public void RemoveCurrency(int amount)
    {
        if (HasEnoughCurrency(amount))
        {
            currency -= amount;
        }
        SaveCurrency();
        currencyUI.UpdateUI();
    }
    public bool HasEnoughCurrency(int amount)
    {
        return currency >= amount;
    }

    public void SaveCurrency()
    {
        CurrencyData data = new CurrencyData();
        data.currency = currency;

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(savePath, jsonData);
    }
    public void LoadCurrency()
    {
        if (File.Exists(savePath))
        {
            string jsonData = File.ReadAllText(savePath);
            CurrencyData data = JsonUtility.FromJson<CurrencyData>(jsonData);
            currency = data.currency;
        }
        else
        {
            currency = 250;
        }
        currencyUI.UpdateUI();
    }
    [System.Serializable]
    struct CurrencyData
    {
        public int currency;
    }
}
