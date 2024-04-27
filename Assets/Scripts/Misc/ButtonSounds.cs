using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => AudioManager.instance.PlaySFX(AudioManager.instance.buttonClick));
    }
    public void HoverSound()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.buttonHover);
    }
}
