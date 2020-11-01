using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Code from COMP260 Week 10 prac
public class OptionMenu : MonoBehaviour
{
    public GameObject optionsPanel;
    public Dropdown qualityDropdown;
    public Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    //public Slider volumeSlider;
    //public Slider musicVolumeSlider;



    // Start is called before the first frame update
    void Start()
    {

        // options panel is initially hidden
        optionsPanel.SetActive(false);


        // populate the list of video quality levels
        qualityDropdown.ClearOptions();
        List<string> names = new List<string>();
        for (int i = 0; i < QualitySettings.names.Length; i++)
        {
            names.Add(QualitySettings.names[i]);
        }
        qualityDropdown.AddOptions(names);

        // populate the list of available resolutions
        resolutionDropdown.ClearOptions();
        List<string> resolutions = new List<string>();
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            resolutions.Add(Screen.resolutions[i].ToString());
        }
        resolutionDropdown.AddOptions(resolutions);




    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPressedOptions()
    {
        // show the options panel

        optionsPanel.SetActive(true);

        // select the current resolution
        int currentResolution = 0;
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            if (Screen.resolutions[i].width == Screen.width &&
                Screen.resolutions[i].height == Screen.height)
            {
                currentResolution = i;
                break;
            }
        }
        resolutionDropdown.value = currentResolution;

        // set the fullscreen toggle
        fullScreenToggle.isOn = Screen.fullScreen;

    }

    public void OnPressedCancel()
    {

        optionsPanel.SetActive(false);

        // select the current quality value
        qualityDropdown.value = QualitySettings.GetQualityLevel();
    }

    public void OnPressedApply()
    {
        // apply the changes
        QualitySettings.SetQualityLevel(qualityDropdown.value);
        Resolution res = Screen.resolutions[resolutionDropdown.value];
        Screen.SetResolution(res.width, res.height, true);

        optionsPanel.SetActive(false);

        Screen.SetResolution(res.width, res.height, fullScreenToggle.isOn);

    }

}
