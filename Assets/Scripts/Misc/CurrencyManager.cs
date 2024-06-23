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
    [SerializeField] TextMeshProUGUI messageUI;
    private void OnDisable()
    {
        SaveCurrency();
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            savePath = Path.Combine(Application.persistentDataPath + "/currency");
            LoadCurrency();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        LoadCurrency();
    }
    public void AddCurrency(int amount)
    {
        currency += amount;
        SaveCurrency();
    }
    public void RemoveCurrency(int amount)
    {
        if (!HasEnoughCurrency(amount))
        {
            SetMessage("You don't have enough SP");
            return;
        }
        currency -= amount;
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
    public void SetMessage(string message)
    {
        messageUI.text = message;
        StartCoroutine(ResetMessage());
    }
    public IEnumerator ResetMessage()
    {
        yield return new WaitForSeconds(3);
        messageUI.text = "";
        StopAllCoroutines();
    }
    [System.Serializable]
    struct CurrencyData
    {
        public int currency;
    }
}
