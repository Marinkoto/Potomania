using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] public float health = 1f, maxHealth;
    [Header("Misc")]
    [SerializeField] GameObject deathFX;
    [SerializeField] Material whiteMat;
    [SerializeField] Material defaultMat;
    [Header("Components")]
    [SerializeField] SpriteRenderer sr;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
        health = maxHealth;
    }

    private void ResetMaterial()
    {
        sr.material = defaultMat;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        sr.material = whiteMat;
        Invoke("ResetMaterial", 0.15f);
        Die();
    }
    public void IncreaseHealth(float amount)
    {
        maxHealth += amount;
    }
    private void Die()
    {
        if(health <= 0)
        {
            GameObject effect = Instantiate(deathFX,transform.position,Quaternion.identity);
            Destroy(effect, 0.5f);
            Destroy(gameObject,0.05f);
        }
    }
}
