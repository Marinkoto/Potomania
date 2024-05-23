using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] bool isActive = false;
    [SerializeField] float currentTime;
    [SerializeField] float upgradeInterval = 300f;
    [SerializeField] float upgradeIntervalBoss = 200f;
    [HideInInspector] float upgradeTimer = 0f;
    [HideInInspector] float upgradeTimerBoss = 0f;
    [HideInInspector] int indexEnemy = 0;
    [HideInInspector] int indexBoss = 0;
    [Header("Components")]
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] EnemyHolder enemyHolder;
    private void Start()
    {
        currentTime = 0;
        TimerState(true);
        AudioManager.instance.MusicSource.volume = 0.05f;
    }
    private void Update()
    {
        CountTime();
        UpgradeEnemy();
        UpgradeBoss();
    }

    private void CountTime()
    {
        if (isActive)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        timeText.text = time.ToString(@"mm\:ss\:ff");
    }
    public void TimerState(bool active)
    {
        isActive = active;
    }
    private void UpgradeEnemy()
    {
        upgradeTimer += Time.deltaTime;
        if (upgradeTimer >= upgradeInterval)
        {
            indexEnemy++;
            enemyManager.enemyPrefab = enemyHolder.enemies[indexEnemy];
            upgradeTimer = 0;
        }
    }
    private void UpgradeBoss()
    {
        upgradeTimerBoss += Time.deltaTime;
        if (upgradeTimerBoss >= upgradeIntervalBoss)
        {
            indexBoss++;
            enemyManager.bossPrefab = enemyHolder.bosses?[indexBoss];
            upgradeTimerBoss = 0;
        }
    }
}
