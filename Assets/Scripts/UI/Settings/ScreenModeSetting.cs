using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenModeSetting : MonoBehaviour
{
    [SerializeField]
    private TMPro.TMP_Dropdown ScreenModeDropDown;
    private Resolutions resolutions;

    void Start()
    {
        resolutions = FindObjectOfType<Resolutions>();
        int val = PlayerPrefs.GetInt("ScreenMode");
        ScreenModeDropDown.value = val;
    }

    public void SetScreenMode(int index)
    {
        PlayerPrefs.SetInt("ScreenMode", index);
        switch (index) 
        {
            case 0:
                Screen.SetResolution(resolutions.resolution.width, resolutions.resolution.height, FullScreenMode.ExclusiveFullScreen);
                break;

            case 1:
                Screen.SetResolution(resolutions.resolution.width, resolutions.resolution.height, FullScreenMode.FullScreenWindow);
                break;

            case 2:
                Screen.SetResolution(resolutions.resolution.width, resolutions.resolution.height, FullScreenMode.Windowed);
                break;
        }
    }
}
