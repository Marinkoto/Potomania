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
    [Header("Components")]
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] EnemyManager enemyManager;
    [SerializeField] EnemyHolder enemyHolder;

    public float upgradeTimer = 0f;
    private int index = 0;

    private void Start()
    {
        currentTime = 0;
        TimerState(true);
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
        if(upgradeTimer >= upgradeInterval)
        {
            index++;
            enemyManager.enemyPrefab = enemyHolder.enemies[index];
            upgradeTimer = 0;
        }
    }
    private void UpgradeBoss()
    {
        upgradeTimer += Time.deltaTime;
        if (upgradeTimer >= upgradeInterval + 200)
        {
            index++;
            enemyManager.bossPrefab = enemyHolder.bosses[index];
            upgradeTimer = 0;
        }
    }
}
