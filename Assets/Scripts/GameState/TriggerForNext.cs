//  THIS IS FOR DEBUG PURPOSES ONLY  DO NOT USE
using UnityEngine;

public class TriggerForNext : MonoBehaviour
{
    [Tooltip("Next Scene is a debug tool to see scenes progressing")]
    bool nextScene;

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
        var spawn = FindObjectOfType<SpawnManager>();
        spawn.HandleNewLap(false);
    }
}
