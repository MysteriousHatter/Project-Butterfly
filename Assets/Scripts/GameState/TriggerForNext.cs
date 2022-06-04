//  THIS IS FOR DEBUG PURPOSES ONLY  DO NOT USE
using UnityEngine;

public class TriggerForNext : MonoBehaviour
{
    [Tooltip("Next Scene is a debug tool to see scenes progressing")]
    bool nextScene;

    /// <summary>
    /// Debug Purposes
    /// </summary>
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            nextScene = true;
        }
    }

    /// <summary>
    /// Used as a sample on how to have whatever trigger resets the laps reset it.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        if(other.tag != "Player")
        {
            return;
        }
      //  var spawn = FindObjectOfType<SpawnManager>();
       // spawn.HandleNewLap(nextScene);
    }
}
