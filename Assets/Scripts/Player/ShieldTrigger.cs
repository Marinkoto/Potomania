using System.Collections;
using UnityEngine;

public class ShieldTrigger : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] float duration = 2f;
    [SerializeField] float cooldown = 5f;
    [SerializeField] public Color shieldColor = Color.cyan;
    [SerializeField] float blinkDuration = 0.05f;
    [SerializeField] int numBlinks = 5;

    [Header("Components")]
    [SerializeField] SpriteRenderer sr;
    [SerializeField] PlayerHealth playerHealth;
    [SerializeField] ParticleSystem shieldParticle;
    [SerializeField] Animator shieldAnimator;

    public bool shieldActive = false;
    private float nextTimeToUse = 0f;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();

        nextTimeToUse = Time.time;

        shieldParticle.gameObject.SetActive(true);
        shieldAnimator.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!shieldActive && Time.time >= nextTimeToUse)
        {
            ActivateShield();
        }
    }

    private void ActivateShield()
    {
        shieldActive = true;
        sr.color = shieldColor;
        playerHealth.canBeDamaged = false;
        shieldParticle.Play();
        shieldAnimator.SetBool("isActive", shieldActive);
        StartCoroutine(DeactivateShieldAfterDuration());
    }

    private IEnumerator DeactivateShieldAfterDuration()
    {
        yield return new WaitForSeconds(duration);

        StartCoroutine(BlinkPlayer());

        yield return new WaitForSeconds(blinkDuration * numBlinks * 2);
        DeactivateShield();

        nextTimeToUse = Time.time + cooldown;
    }

    private void DeactivateShield()
    {
        sr.color = Color.white;
        playerHealth.canBeDamaged = true;
        shieldActive = false;
        shieldParticle.Play();
        shieldAnimator.SetBool("isActive", shieldActive);
    }

    private IEnumerator BlinkPlayer()
    {
        for (int i = 0; i < numBlinks; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(blinkDuration);

            sr.enabled = true;
            yield return new WaitForSeconds(blinkDuration);
        }
    }
}