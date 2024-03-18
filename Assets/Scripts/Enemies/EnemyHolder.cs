using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHolder : MonoBehaviour
{
    [SerializeField] public GameObject[] enemies;
    [SerializeField] public GameObject[] bosses;
    public void Start()
    {
        enemies[0].GetComponent<EnemyHealth>().health = PlayerStats.Instance.damage;
        enemies[1].GetComponent<EnemyHealth>().health = PlayerStats.Instance.damage + 1f;
        enemies[2].GetComponent<EnemyHealth>().health = PlayerStats.Instance.damage + 2.5f;
        enemies[3].GetComponent<EnemyHealth>().health = PlayerStats.Instance.damage + 1.5f;
        enemies[4].GetComponent<EnemyHealth>().health = PlayerStats.Instance.damage + 3f;
        bosses[0].GetComponent<EnemyHealth>().health = PlayerStats.Instance.damage + 2f;
        bosses[1].GetComponent<EnemyHealth>().health = (PlayerStats.Instance.damage * 2f) + 3f;
        bosses[2].GetComponent<EnemyHealth>().health = (PlayerStats.Instance.damage * 1.5f) + 4.5f;
    }
}
