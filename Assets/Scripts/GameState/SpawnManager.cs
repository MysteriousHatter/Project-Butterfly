using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [Tooltip("The current scene to spawn in")]
    public Object currentScene; 

    [Tooltip("Place all the lap specific objects here")]
    [SerializeField] Object[] sceneList = new Object[3];

    [Tooltip("Seralized for Debug Purposes")]
    [SerializeField] int lapCount;

    private string baseName;

    private void Start()
    {
        baseName = SceneManager.GetActiveScene().name;
        if (sceneList.Length == 0)
            return;
        currentScene = sceneList[0];
        SceneManager.LoadScene(currentScene.name, LoadSceneMode.Additive);
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
            if(SceneManager.GetSceneAt(i).name != baseName)
            {
                unload.Add(SceneManager.GetSceneAt(i));
            }
        }
        if(progress)
        {
            lapCount++;
            if(lapCount < sceneList.Length)
            {
                currentScene = sceneList[lapCount];
            }
        }
        foreach(Scene scene in unload)
        {
            SceneManager.UnloadSceneAsync(scene);
        }

        SceneManager.LoadSceneAsync(currentScene.name, LoadSceneMode.Additive);
    }
}
