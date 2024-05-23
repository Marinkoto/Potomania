using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHolder : MonoBehaviour
{
    [SerializeField] public GameObject[] enemies;
    [SerializeField] public GameObject[] bosses;
    public void Start()
    {
        enemies[0].GetComponent<EnemyHealth>().maxHealth = PlayerStats.Instance.damage;
        enemies[1].GetComponent<EnemyHealth>().maxHealth = PlayerStats.Instance.damage + 2f;
        enemies[2].GetComponent<EnemyHealth>().maxHealth = PlayerStats.Instance.damage + 3.5f;
        enemies[3].GetComponent<EnemyHealth>().maxHealth = PlayerStats.Instance.damage + 4.5f;
        enemies[4].GetComponent<EnemyHealth>().maxHealth = PlayerStats.Instance.damage + 5f;
        bosses[0].GetComponent<EnemyHealth>().maxHealth = PlayerStats.Instance.damage + 4f;
        bosses[1].GetComponent<EnemyHealth>().maxHealth = (PlayerStats.Instance.damage * 2f) + 4f;
        bosses[2].GetComponent<EnemyHealth>().maxHealth = (PlayerStats.Instance.damage * 1.75f) + 5.5f;
    }
}
