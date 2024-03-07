using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burp : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] ParticleSystem[] burpParticles;
    [SerializeField] SpriteRenderer spriteRenderer;
    [Header("Parameters")]
    [SerializeField] float cooldown = 3f;
    [Header("References")]
    [SerializeField] ShieldTrigger shield;

    private float nextTimeToUse = 0f;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        shield = GetComponent<ShieldTrigger>();
        nextTimeToUse = Time.time + cooldown;
        foreach(var particle in burpParticles)
        {
            particle.gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if(Time.time >= nextTimeToUse)
        {
            StartCoroutine(UseBurp());
            nextTimeToUse = Time.time + cooldown;
        }
    }
    private IEnumerator UseBurp()
    {
        spriteRenderer.color = Color.green;
        yield return new WaitForSeconds(0.8f);
        foreach (ParticleSystem burp in burpParticles)
        {
            burp.Play();
        }
        yield return new WaitForSeconds(0.1f);
        if (shield.shieldActive && shield.enabled)
        {
            spriteRenderer.color = shield.shieldColor;
        }
        else
        {
            spriteRenderer.color = Color.white;
        }
    }
}
