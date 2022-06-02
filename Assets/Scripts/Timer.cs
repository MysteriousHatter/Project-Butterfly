using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    List<GameObject> completedCheckpoints = new List<GameObject>();

    [Tooltip("Please ensure this Array contains the amount of checkpoints in the level if more than 10")]
    public float[] timesAtCheckpoint = new float[10];

    private int currentCheckpoint = 0;

    GameplayUIBehavior gameplayUIBehavior;

    private void Start()
    {
        gameplayUIBehavior = FindObjectOfType<GameplayUIBehavior>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            CheckPointPassed(this.gameObject);
        }
    }
    /// <summary>
    /// Call this when a checkpoint has been passed.
    /// </summary>
    /// <param name="checkpointObject">The Gameobject which is the checkpoint.  Used to ensure it doesn't double trigger</param>
    public void CheckPointPassed(GameObject checkpointObject)
    {
        /*if(completedCheckpoints.Contains(checkpointObject))
        {
            return;
        }*/
        float timePassed = gameplayUIBehavior.TimeTotal - gameplayUIBehavior.TimeLeft;
        foreach(float pastTimes in timesAtCheckpoint)
        {
            timePassed -= pastTimes;
        }
        timesAtCheckpoint[currentCheckpoint] = timePassed;
        currentCheckpoint++;
        completedCheckpoints.Add(checkpointObject);
        gameplayUIBehavior.UpdateTime(timePassed);
    }
}
