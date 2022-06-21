using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSettingsScript : MonoBehaviour
{    
    public void SetMusicVolume(float volLevel)
    {
        AkSoundEngine.SetRTPCValue("MusicVolume", volLevel);
        AkSoundEngine.SetRTPCValue("SFXVolume", volLevel);
    }

    public void MuteAllAudio(bool toggleValue)
    {
        if (toggleValue)
        {
            AkSoundEngine.SetState("VolState", "Mute");
        }
        else
        {
            AkSoundEngine.SetState("VolState", "None");
        }
    }
}
