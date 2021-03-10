using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Graphics : MonoBehaviour
{
    public Dropdown resolutionSetting;
    public Toggle FullscreenToggle;

    Resolution[] resolutions;

    private void Start()
    {
        FullscreenToggle.isOn = Screen.fullScreen;

        resolutions = Screen.resolutions;
        resolutionSetting.ClearOptions();

        List<string> options = new List<string>();

        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionSetting.AddOptions(options);
        resolutionSetting.value = currentResIndex;
        resolutionSetting.RefreshShownValue();
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resIndex)
    {
        Resolution res = resolutions[resIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }
}
