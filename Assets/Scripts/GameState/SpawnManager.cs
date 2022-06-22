using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [Tooltip("The current scene to spawn in")]
    public int currentScene;
   // int sceneCount = 0;

    [Tooltip("Place all the lap specific objects here")]
    [SerializeField] int[] sceneList = new int[3];

    [Tooltip("Seralized for Debug Purposes")]
    [SerializeField] int lapCount;

    private int baseNumber;

    CheckpointHandler checkpointHandler;

    private void Start()
    {
        baseNumber = SceneManager.GetActiveScene().buildIndex;
        if (sceneList.Length == 0)
            return;
        currentScene = sceneList[0];
        Debug.Log("The current scene " + currentScene);
        SceneManager.LoadScene(currentScene, LoadSceneMode.Additive);
        checkpointHandler = FindObjectOfType<CheckpointHandler>();
    }

    /// <summary>
    /// Handles loading a new lap
    /// </summary>
    /// <param name="progress">If true, goes to the next lap, otherwise repeats the lap</param>
    public void HandleNewLap(bool progress)
    {
        List<Scene> unload = new List<Scene>();
        for(int i = 0; i < SceneManager.sceneCount; i++)
        {
            if(SceneManager.GetSceneAt(i).buildIndex != baseNumber)
            {
                unload.Add(SceneManager.GetSceneAt(i));
            }
        }
        if(progress)
        {
            checkpointHandler.CheckPointPassed();
            lapCount++;
            if (lapCount < sceneList.Length)
            {
                currentScene = sceneList[lapCount];
            }

            foreach (Scene scene in unload)
            {
                SceneManager.UnloadSceneAsync(scene);
            }

            SceneManager.LoadSceneAsync(currentScene, LoadSceneMode.Additive);
            return;
        }

    }
}
