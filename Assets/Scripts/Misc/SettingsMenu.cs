using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [Header("Resolution Settings")]
    [SerializeField] TMP_Dropdown resolutionDropdown;

    [Header("VSync Settings")]
    [SerializeField] Toggle vsyncToggle;
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Toggle musicToggle;
    [SerializeField] Toggle sfxToggle;
    Resolution[] resolutions;
    public void Awake()
    {
        LoadResolutionDropdown();
        LoadToggle("Vsync", vsyncToggle);
        LoadToggle("Fullscreen", fullscreenToggle);
        LoadToggle("SFX", sfxToggle);
        LoadToggle("Music", musicToggle);
    }

    void LoadResolutionDropdown()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = $"{resolutions[i].width}x{resolutions[i].height}";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResIndex;
        resolutionDropdown.RefreshShownValue();
    }

    void LoadToggle(string Key, Toggle toggle)
    {
        bool vsyncEnabled = Convert.ToBoolean(PlayerPrefs.GetInt(Key, 0));
        toggle.isOn = vsyncEnabled;
        SetVsync(vsyncEnabled);
    }

    public void SetResolution(int resolutionIndex)
    {
        if (resolutions == null || resolutionIndex < 0 || resolutionIndex >= resolutions.Length)
        {
            return;
        }

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height,Screen.fullScreen);
    }
    public void SetSfxToggle(bool enabler)
    {
        int keyValue = Convert.ToInt32(enabler);
        sfxToggle.isOn = enabler;
        PlayerPrefs.SetInt("SFX", keyValue);
    }
    public void SetMusicToggle(bool enabler)
    {
        int keyValue = Convert.ToInt32(enabler);
        musicToggle.isOn = enabler;
        PlayerPrefs.SetInt("Music", keyValue);
    }
    public void SetFullscreen(bool fullscreen)
    {
        int fullscreenCount = Convert.ToInt32(fullscreen);
        Screen.fullScreen = fullscreen;
        PlayerPrefs.SetInt("Fullscreen", fullscreenCount);
    }

    public void SetVsync(bool vsync)
    {
        int vsyncCount = Convert.ToInt32(vsync);
        QualitySettings.vSyncCount = vsyncCount;
        PlayerPrefs.SetInt("Vsync", vsyncCount);
    }
}
