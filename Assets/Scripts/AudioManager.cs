using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("Audio Source")]
    [SerializeField] public AudioSource MusicSource;
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource HitSFXSource;
    [SerializeField] AudioSource LevelUPSFXSource;
    [SerializeField] AudioSource ShootFXSource;
    [Header("Audio Clips")]
    [SerializeField] public AudioClip menuMusic;
    [SerializeField] public AudioClip enemyHit;
    [SerializeField] public AudioClip playerHit;
    [SerializeField] public AudioClip levelUp;
    [SerializeField] public AudioClip buttonClick;
    [SerializeField] public AudioClip buttonHover;
    [SerializeField] public AudioClip itemPickUp;
    [SerializeField] public AudioClip[] burps;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(this);
        MusicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        if(!SFXSource.isPlaying && SFXSource.isActiveAndEnabled)
        {
            SFXSource.clip = clip;
            SFXSource.Play();
        }
    }
    public void PlayHitSFX(AudioClip clip)
    {
        if (!HitSFXSource.isPlaying && SFXSource.isActiveAndEnabled)
        {
            HitSFXSource.clip = clip;
            HitSFXSource.Play();
        }
    }
    public void PlayLevelUPSFX(AudioClip clip)
    {
        if(!LevelUPSFXSource.isPlaying && LevelUPSFXSource.isActiveAndEnabled)
        {
            LevelUPSFXSource.clip = clip;
            LevelUPSFXSource.Play();
        }
    }
}
