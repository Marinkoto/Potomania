using CodeMonkey.MonoBehaviours;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] int expAmount;
    [Header("Components")]
    [SerializeField] Sprite lootSprite;

    private float distance;

    private void Start()
    {
        SetLoot();
    }
    private void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, PlayerStats.Instance.transform.position);
        if (distance <= PlayerStats.Instance.pickUpRange)
        {
            Follow();
        }
        Destroy(gameObject, 50);
    }

    private void Follow()
    {
        transform.position = Vector2.Lerp(transform.position, PlayerStats.Instance.transform.position,
            UnityEngine.Random.Range(2, 6) * Time.deltaTime);
        Animate();
        if (distance < 0.9f)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.itemPickUp);
            ExperienceManager.instance.AddExp(expAmount);
            Destroy(gameObject);
        }
    }
    private void SetLoot()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = lootSprite;
    }
    private void Animate()
    {
        transform.Rotate(0, 0, 360 * Time.deltaTime);
    }
}
