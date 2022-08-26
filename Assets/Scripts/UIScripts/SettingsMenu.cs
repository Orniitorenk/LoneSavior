using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMix;
    private bool isFullscreen = true;

    public void SetResolution(int index)
    {
        if(index == 0)
        {
            Screen.SetResolution(1920, 1080, isFullscreen);
        }
        else if(index == 1)
        {
            Screen.SetResolution(1366, 768, isFullscreen);
        }
        else
        {
            Screen.SetResolution(1440, 900, isFullscreen);
        }
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool fullscreenEnable)
    {
        Screen.fullScreen = fullscreenEnable;
        isFullscreen = fullscreenEnable;
    }

    public void SetMasterVolume(float value)
    {
        audioMix.SetFloat("MasterVolume", value);
    }

    public void SetMusicVolume(float value)
    {
        audioMix.SetFloat("MusicVolume", value);
    }

}
