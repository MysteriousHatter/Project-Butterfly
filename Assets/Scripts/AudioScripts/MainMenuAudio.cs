using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAudio : MonoBehaviour
{
    public void HighlightButtonSFX()
    {
        AkSoundEngine.PostEvent("MenuSelectHighlight", this.gameObject);
    }
}
