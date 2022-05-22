using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VideoMenu : MonoBehaviour
{
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown fullscreenDropdown;
    public TMP_Dropdown qualityDropdown;

    Resolution[] resolutions;

    public void Start()
    {
        CalculateResolutions();
        SetDropdowns();
    }

    private void SetDropdowns()
    {
        resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
        fullscreenDropdown.value = PlayerPrefs.GetInt("Fullscreen");
        qualityDropdown.value = PlayerPrefs.GetInt("Quality");
    }

    public void CalculateResolutions()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";

            options.Add(option);

            if (resolutions[i].width == resolutions[PlayerPrefs.GetInt("Resolution")].width &&
                resolutions[i].height == resolutions[PlayerPrefs.GetInt("Resolution")].height &&
                resolutions[i].refreshRate == resolutions[PlayerPrefs.GetInt("Resolution")].refreshRate)
            {
                resolutionDropdown.value = PlayerPrefs.GetInt("Resolution");
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();
    }

    public void Resolution(int index)
    {
        Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreen, resolutions[index].refreshRate);
        PlayerPrefs.SetInt("Resolution", resolutionDropdown.value);
    }

    public void Fullscreen(int index)
    {
        Screen.fullScreenMode = (FullScreenMode)index;
        PlayerPrefs.SetInt("Fullscreen", fullscreenDropdown.value);
    }

    public void Quailty(int index)
    {
        QualitySettings.SetQualityLevel(index);
        PlayerPrefs.SetInt("Quality", qualityDropdown.value);
    }
}