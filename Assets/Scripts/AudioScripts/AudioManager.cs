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

    public void MusicState()
    {
        // get scene name
        AkSoundEngine.SetState("GameState", "InMenu");
        // set state based on scene name
    }

    public void PickupSFX()
    {
        // detect collision between playerChar and x
        // play pickup event

    }

    public void HazardSFX()
    {
        // detect collision between playerChar and y
        // play hazard event
    }

    public void VortexSFX()
    {
        // detect vortex generation
        // play vortex event
    }

    public void BoostSFX()
    {
        // detect collision with booster
        // play boost event
    }

    public void LapClearedSFX()
    {
        // detect new lap
        // play lap cleared event
    }

    public void StageClearedSFX()
    {
        // detect player winning
        // play stage cleared event
    }

    public void StatueSFX()
    {
        // detect statue status
        // play statue event
    }
}
