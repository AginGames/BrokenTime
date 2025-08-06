using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using System.Reflection.Emit;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public Toggle toggle;
    Resolution[] resolutions;
    private void Start()
    {
        if(Screen.fullScreen == true)
        {
            toggle.isOn = true;
        }

        if (QualitySettings.GetQualityLevel() == 0)
        {
            dropdown.value = 0;
        }
        else if (QualitySettings.GetQualityLevel() == 1)
        {
            dropdown.value =  1;
        }
        else if (QualitySettings.GetQualityLevel() == 2)
        {
            dropdown.value =  2;
        }
        else if (QualitySettings.GetQualityLevel() == 3)
        {
            dropdown.value = 3;
        }

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> resolutionList = new List<string>();

        int currentRes = 0;

        for(int i= 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            resolutionList.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }

        resolutionDropdown.AddOptions(resolutionList);
        resolutionDropdown.value = currentRes;
        resolutionDropdown.RefreshShownValue();
    }
    public void SerRes(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume (float volume)
    {
        audioMixer.SetFloat("MasterAudio", volume);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void fullscreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }
}
