using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] public bool canBeDamaged;
    private void Start()
    {
        canBeDamaged = true;
    }
    public void TakeDamage(int amount)
    {
        if (canBeDamaged)
        {
            PlayerStats.Instance.playerHealth -= amount;
            Die();
        }
    }
    private void Die()
    {
        if (PlayerStats.Instance.playerHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
