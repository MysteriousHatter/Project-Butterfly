using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        AkSoundEngine.PostEvent("Play_MusicTheme", gameObject);
        // load music event 1 time
        // do not destroy until game is destroyed
    }

    public void PickupSFX()
    {
        // detect collision between playerChar and x
        // play pickup event
        AkSoundEngine.PostEvent("PlayerPickup", gameObject);
    }

    public void HazardSFX()
    {
        // detect collision between playerChar and y
        // play hazard event
        AkSoundEngine.PostEvent("PlayerHurt", gameObject);
    }

    public void VortexSFX()
    {
        // detect vortex generation
        // play vortex event
        AkSoundEngine.PostEvent("PlayerVortex", gameObject);
    }

    public void BoostSFX()
    {
        // detect collision with booster
        // play boost event
        AkSoundEngine.PostEvent("SpeedBoost", gameObject);
    }

    public void LapClearedSFX()
    {
        // detect new lap
        // play lap cleared event
        AkSoundEngine.PostEvent("LapCleared", gameObject);
    }

    public void StageClearedSFX()
    {
        // detect player winning
        // play stage cleared event
        AkSoundEngine.PostEvent("StageCleared", gameObject);
    }

    public void StatueSFX()
    {
        // detect statue status
        // play statue event
        AkSoundEngine.PostEvent("StatueFreed", gameObject);
    }

    public void SetMusicVolume(float volLevel)
    {
        AkSoundEngine.SetRTPCValue("MusicVolume", volLevel);
    }

    public void SetSFXVolume(float sfxLevel)
    {
        AkSoundEngine.SetRTPCValue("SFXVolume", sfxLevel);
    }
}
