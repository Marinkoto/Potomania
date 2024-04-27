using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottles : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] public GameObject bottlePrefab;
    [Header("Parameters")]
    [SerializeField] public float shootSpeed;
    [SerializeField] public float shootDelay;

    private void Start()
    {
        InvokeRepeating("Shoot", 0.5f, shootDelay);
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPoint.gameObject.SetActive(true);
        }
    }
    private void Shoot()
    {
        foreach (Transform point in spawnPoints)
        {
            GameObject bottle = Instantiate(bottlePrefab, point.position, point.rotation);
            Rigidbody2D rb = bottle.GetComponent<Rigidbody2D>();
            int rnd = Random.Range(0, 2);
            if(rnd == 0)
            {
                rb.velocity = Vector2.up * shootSpeed;
            }
            if (rnd == 1)
            {
                rb.velocity = Vector2.down * shootSpeed;
            }
            Destroy(bottle, 4f);
        }
    }
}
