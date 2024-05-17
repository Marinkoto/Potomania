using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TimeController : MonoBehaviour
{
    private void Start()
    {
        Camera.main.GetComponent<UniversalAdditionalCameraData>().renderPostProcessing = false;
    }
    public void StartTime()
    {
        Time.timeScale = 1.0f;
    }
    public void StopTime()
    {
        Time.timeScale = 0.0f;
    }
}
