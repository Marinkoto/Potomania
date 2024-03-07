using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] GameObject molotovPrefab;
    [Header("Parameters")]
    [SerializeField] float cooldown = 3f;
    [SerializeField] float circleRadius = 5f;

    private float nextTimeToUse = 0f; 

    private void Start()
    {
        nextTimeToUse = Time.time + cooldown; 
    }

    private void Update()
    {
        if (Time.time >= nextTimeToUse)
        {
            UseMolotov();
            nextTimeToUse = Time.time + cooldown; 
        }
    }

    private void UseMolotov()
    {
        Vector2 randomCirclePoint = Random.insideUnitCircle.normalized * circleRadius;
        Vector3 spawnPosition = new Vector3(randomCirclePoint.x, randomCirclePoint.y, 0f) + transform.position;
        GameObject molotovInstance = Instantiate(molotovPrefab, spawnPosition, Quaternion.identity);
        Destroy(molotovInstance,4f);
    }
}
