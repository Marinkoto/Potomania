using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    public static CameraShake instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    private void Start()
    {
        ShakeCamera(0, 0, 0);
        virtualCamera.m_Lens.OrthographicSize = 6.5f;
    }
    public void ShakeCamera(float frequency, float amplitude, float duration)
    {
        float seed = Time.time * frequency;
        float noiseValue = Mathf.PerlinNoise(seed, 0f);

        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = noiseValue * 10f;

        Invoke("StopShaking", duration);
    }
    private void StopShaking()
    {
        noise.m_AmplitudeGain = 0f;
    }
}
