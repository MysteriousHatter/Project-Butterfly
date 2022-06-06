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

    public PathCreation.PathCreator[] paths;
    private static GameplayManager instance;

    private bool CurrentStatueUnlocked { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        CurrentStatueUnlocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
