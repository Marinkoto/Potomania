using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHoverSound : MonoBehaviour
{
    public void PlaySound()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonHover);
    }
}
