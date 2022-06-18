using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Slider slide;
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeVolume(float newVolume)
    {
        float vol = newVolume;
        PlayerPrefs.SetFloat("volume", vol);
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        print("Vol");
    }
}
