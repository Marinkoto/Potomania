using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiusCheck : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] GameObject buffedFX;
    [SerializeField] GameObject nerfedFX;

    //If the player enters the whisky radius they get increased stats
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerStats player))
        {
            player.moveSpeed = player.moveSpeedInRadius;
            player.fireRate = player.fireRateInRadius;
            ActivateEffect(buffedFX);
            DisableEffect(nerfedFX);
        }
    }

    //If the player enters the whisky radius they get decreased stats
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerStats player))
        {
            player.moveSpeed = player.moveSpeedOutRadius;
            player.fireRate = player.fireRateOutRadius;
            ActivateEffect(nerfedFX);
            DisableEffect(buffedFX);
        }
    }
    private void DisableEffect(GameObject effect)
    {
        effect.SetActive(false);
    }
    private void ActivateEffect(GameObject effect)
    {
        effect.SetActive(true);
    }
}
