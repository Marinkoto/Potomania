using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemies + Spawn Points")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] List<GameObject> enemyPrefabs;
    [SerializeField] GameObject bossPrefab;
    [Header("Parameters")]
    [SerializeField] int wavesCount = 5;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float timeBetweenEnemies = 1.5f;
    [SerializeField] int enemiesPerWave = 5;
    [SerializeField] AnimationCurve difficultyCurve;

    private int currentWave = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave < wavesCount)
        {
            yield return new WaitForSeconds(timeBetweenWaves);
            SpawnWave();
            currentWave++;
        }
    }

    void SpawnWave()
    {
        int currentEnemies = Mathf.RoundToInt(difficultyCurve.Evaluate(currentWave / (float)wavesCount) * enemiesPerWave);

        for (int i = 0; i < currentEnemies; i++)
        {
            int randomEnemyIndex = Random.Range(0, enemyPrefabs.Count);
            GameObject enemyPrefab = enemyPrefabs[randomEnemyIndex];

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position + new Vector3(Random.Range(0.05f,0.1f), Random.Range(0.05f, 0.1f)), spawnPoint.rotation);
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
        }
        if (currentWave % 3 == 0)
        {
            StartCoroutine(SpawnBoss());
        }
    }

    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(timeBetweenEnemies);

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
        enemiesPerWave++;
    }
}

