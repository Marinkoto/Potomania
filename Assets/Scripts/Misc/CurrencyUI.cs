using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] TextMeshProUGUI currencyCounter;
    void Start()
    {
        UpdateUI();
    }
    public void UpdateUI()
    {
        currencyCounter.text = $"{CurrencyManager.instance.currency} SP";
    }
}
