using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public uint playingID;

    public void menuSFX()
    {
        AkSoundEngine.PostEvent("MenuSelectHighlight", this.gameObject);
    }
}
