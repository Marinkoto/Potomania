using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    [Header("Instances and Events")]
    [SerializeField] public static ExperienceManager instance;
    [SerializeField] public delegate void ExperienceChangeHandler(int amount);
    [SerializeField] public event ExperienceChangeHandler OnExperienceChange;
    [Header("UI Components")]
    [SerializeField] private Slider expBar;
    [SerializeField] private TextMeshProUGUI levelText, expCounter;
    private void Awake()
    {
        instance = this;
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void AddExp(int amount)
    {
        OnExperienceChange?.Invoke(amount);
    }
    public void SetExperienceBar(int currentExp, int maxExp, int level)
    {
        expBar.value = currentExp;
        expBar.maxValue = maxExp;
        levelText.text = $"Level {level}";
        expCounter.text = $"{currentExp}/{maxExp}";
    }
}
