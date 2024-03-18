using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerShoot : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce;
    [SerializeField] float spread;
    [HideInInspector] private float timeStamp;
    [Header("Components")]
    [SerializeField] Transform firePoint;
    [SerializeField] Animator animator;
    [SerializeField] ParticleSystem muzzle;
    [SerializeField] AudioClip shootSound;
    [SerializeField] Volume volume;
    private void Update()
    {
        if (Time.time >= timeStamp && Input.GetMouseButton(0))
        {
            Shoot();
            timeStamp = Time.time + PlayerStats.Instance.fireRate;
        }
    }
    private void Shoot()
    {
        animator.SetTrigger("Shoot");
        muzzle.Play();
        CameraShake.instance.ShakeCamera(0.05f, 0.05f, 0.1f);
        AudioManager.instance.PlayShootSFX(shootSound);
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        Vector2 dir = firePoint.transform.right;
        Vector2 pdir = Vector2.Perpendicular(dir) * Random.Range(-spread, spread);
        rb.AddForce((dir + pdir) * bulletForce, ForceMode2D.Impulse);
        Destroy(bullet, 6f);
    }

}
