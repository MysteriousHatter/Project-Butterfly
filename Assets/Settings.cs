using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
public class Settings : MonoBehaviour
{
    public Slider volumeSlider;
    private TMP_Dropdown resolutionDropdown;
    // Start is called before the first frame update
    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnVolumeChanged()
    {
        //TODO: Fix volume settings
        AkSoundEngine.SetRTPCValue("MasterVolume", volumeSlider.value);
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
    }

    public void FullScreenDropBox(TMP_Dropdown dropdown)
    {
        Debug.Log("Option" + dropdown.value);
        //Value 0 = Full Screen
        //Value 1 = Borderless Windowed
        //Value 2 = Windowed
    }

   
}