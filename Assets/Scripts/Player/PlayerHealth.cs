using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] public bool canBeDamaged;
    [Header("Components")] 
    [SerializeField] Slider healthBar;
    private void Start()
    {
        canBeDamaged = true;
        PlayerStats.Instance.playerHealth = PlayerStats.Instance.maxHealth;
        UpdateHealthBar();
    }
    public void TakeDamage(int amount)
    {
        if (canBeDamaged)
        {
            PlayerStats.Instance.playerHealth -= amount;
            UpdateHealthBar();
            Die();
        }
    }
    private void Die()
    {
        if (PlayerStats.Instance.playerHealth <= 0)
        {
            CurrencyManager.instance.AddCurrency(ExperienceManager.instance.enemiesKilled / 2);
            SceneManager.LoadScene(0);
        }
    }
    private void UpdateHealthBar()
    {
        healthBar.maxValue = PlayerStats.Instance.maxHealth;
        healthBar.value = PlayerStats.Instance.playerHealth;
        if (healthBar.value >= healthBar.maxValue)
        {
            healthBar.gameObject.SetActive(false);
        }
        else
        {
            healthBar.gameObject.SetActive(true);
        }
    }
}
