using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    public void StartGameMusic()
    {
        // This should play music event at start of game,
        // but never re-play/re-instantiate when revisiting scenes,
        // and only be destroyed when the game is destroyed.

        // For instance: music plays when game starts in main menu,
        // player visits 'how to play', returns to main menu,
        // music should not restart when revisiting main menu.
        // loading/un-loading scenes, should not affect music event.
        AkSoundEngine.PostEvent("Play_MusicTheme", this.gameObject);
    }


    // In the event that Ak State component does not function in each scene
    // use the two methods below to update Ak State
    public void CheckMusicState(GameObject currentScene)
    {
        // This should check the current scene the AkState,
        // i.e. "InGame" or "InMenu"
    }

    public void UpdateMusicState()
    {
        // This should update the AkState based on CheckMusicState() result.
    }



}
