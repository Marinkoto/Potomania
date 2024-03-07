using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] bool isActive = false;
    [SerializeField] float currentTime;
    [SerializeField] TextMeshProUGUI timeText;
    private void Start()
    {
        currentTime = 0;
        TimerState(true);
    }
    private void Update()
    {
        CountTime();
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
}
