using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Spawn Points + Enemies")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public GameObject bossPrefab;
    [SerializeField] Transform enemyHolder;

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
            StartCoroutine(SpawnWave());
            currentWave++;
        }
    }

    IEnumerator SpawnWave()
    {
        int currentEnemies = Mathf.RoundToInt(difficultyCurve.Evaluate(currentWave / (float)wavesCount) * enemiesPerWave);

        for (int i = 0; i < currentEnemies; i++)
        {
            StartCoroutine(SpawnEnemy());
            yield return new WaitForSeconds(timeBetweenEnemies);
        }

        if (currentWave % 3 == 0)
        {
            StartCoroutine(SpawnBoss());
        }
    }

    IEnumerator SpawnEnemy()
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(enemyPrefab, spawnPoint.position + new Vector3(Random.Range(0.05f, 0.1f), Random.Range(0.05f, 0.1f)), spawnPoint.rotation,enemyHolder);
        yield return null;
    }

    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(timeBetweenEnemies);

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(bossPrefab, spawnPoint.position, spawnPoint.rotation);
        enemiesPerWave++;
    }
}