using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class RadiusCheck : MonoBehaviour
{
    [Header("Effects")]
    [SerializeField] GameObject[] buffedFX;
    [SerializeField] GameObject[] nerfedFX;
    [SerializeField] Volume volume;
    private void Update()
    {
        if (PlayerStats.Instance != null)
        {
            if (CheckDistance() <= 5)
            {
                InRadius();
            }
            else
            {
                OutRadius();
            }
        }
    }
    private float CheckDistance()
    {
        return Vector2.Distance(transform.position, PlayerStats.Instance.transform.position);
    }
    private void InRadius()
    {
        PlayerStats.Instance.moveSpeed = PlayerStats.Instance.moveSpeedInRadius;
        PlayerStats.Instance.fireRate = PlayerStats.Instance.fireRateInRadius;
        volume.profile.TryGet(out ColorAdjustments colorAdjustments);
        colorAdjustments.active = false;
        foreach (var FX in buffedFX)
        {
            Effect(FX, true);
        }
        foreach (var FX in nerfedFX)
        {
            Effect(FX, false);
        }
    }

    private void OutRadius()
    {
        PlayerStats.Instance.moveSpeed = PlayerStats.Instance.moveSpeedOutRadius;
        PlayerStats.Instance.fireRate = PlayerStats.Instance.fireRateOutRadius;
        volume.profile.TryGet(out ColorAdjustments colorAdjustments);
        colorAdjustments.active = true;
        foreach (var FX in buffedFX)
        {
            Effect(FX, false);
        }
        foreach (var FX in nerfedFX)
        {
            Effect(FX, true);
        }
    }
    private void Effect(GameObject effect, bool state)
    {
        effect.SetActive(state);
    }
}
