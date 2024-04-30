using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Resolutions : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Dropdown resolutionDropDown;

    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private int currentResolutionIndex = 0;

    [HideInInspector]
    public Resolution resolution;

    private void Start()
    {
        //Gets all supported resolutions
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropDown.ClearOptions();

        //Checks for existing resolutions and adds unique ones
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (
                !filteredResolutions.Any(x =>
                    x.width == resolutions[i].width && x.height == resolutions[i].height
                )
            )
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();

        //Displays all resolutions in dropdown menu based on available resolutions
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption =
                filteredResolutions[i].width + " x " + filteredResolutions[i].height;
            options.Add(resolutionOption);
            if (
                filteredResolutions[i].width == Screen.width
                && filteredResolutions[i].height == Screen.height
            )
            {
                currentResolutionIndex = i;
            }
        }

        //Settings resolution
        resolutionDropDown.AddOptions(options);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        resolution = filteredResolutions[resolutionIndex];

        //Changes screen mode based on choosen value
        switch (PlayerPrefs.GetInt("ScreenMode"))
        {
            case 0:
                Screen.SetResolution(
                    resolution.width,
                    resolution.height,
                    FullScreenMode.ExclusiveFullScreen
                );
                break;

            case 1:
                Screen.SetResolution(
                    resolution.width,
                    resolution.height,
                    FullScreenMode.FullScreenWindow
                );
                break;

            case 2:
                Screen.SetResolution(resolution.width, resolution.height, FullScreenMode.Windowed);
                break;
        }
    }
}
