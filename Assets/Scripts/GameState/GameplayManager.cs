using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class GameplayManager : MonoBehaviour
{
    public static  GameplayManager Instance
    {
        get
        {
            instance = GameObject.FindObjectOfType<GameplayManager>();
            if (instance == null)
            {
                
                GameObject a = new GameObject("a");
                a.AddComponent<GameplayManager>();
                instance = a.GetComponent<GameplayManager>();

            }
            return instance;
        }
    }

    int currentLap;
    public PathCreation.PathCreator[] paths;
    public int[] orbsNeeded;
    int[] orbsCollected;
    private static GameplayManager instance;
    private SpawnManager spawnManager;

    private bool CurrentStatueUnlocked { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        CurrentStatueUnlocked = false;
        orbsCollected = new int[orbsNeeded.Length];
        spawnManager = FindObjectOfType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateLap()
    {
        if(orbsNeeded[currentLap] <= orbsCollected[currentLap])
        {
            currentLap++;
            spawnManager.HandleNewLap(true);
        }
        else
        {
            spawnManager.HandleNewLap(false);
        }
    }

    public void CollectedOrb()
    {
        orbsCollected[currentLap]++;
    }
}
