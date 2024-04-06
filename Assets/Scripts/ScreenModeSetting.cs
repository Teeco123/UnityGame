using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenModeSetting : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown ScreenModeDropDown;
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
        if (index == 0)
        {
            Screen.SetResolution(resolutions.resolution.width, resolutions.resolution.height, true);

            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        if (index == 1)
        {
            Screen.SetResolution(resolutions.resolution.width, resolutions.resolution.height, false);

            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
