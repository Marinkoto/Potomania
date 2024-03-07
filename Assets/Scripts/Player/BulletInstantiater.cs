using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInstantiater : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] GameObject bulletPrefab;
    [Header("Parameters")]
    [SerializeField] float shootDelay;
    [SerializeField] float lifeTime;
    
    private IEnumerator Start()
    {
        foreach (Transform spawnPoint in spawnPoints)
        {
            spawnPoint.gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(7);
        InvokeRepeating("Shoot", 0.5f, shootDelay);
    }
    private void Shoot()
    {
        for (int i = 0; i < PlayerStats.Instance.bulletAmount; i++)
        {
            foreach (Transform point in spawnPoints)
            {
                GameObject bullet = Instantiate(bulletPrefab, point.position, Quaternion.identity);
                Destroy(bullet, lifeTime);
            }
        }
    }
}
