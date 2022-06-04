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

    private int m_currentCollectedOrb = 0;

    private int m_orbsNeedToUnlockStatue = 20;

    private int m_unlockedStates = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnOrbCollected(int orbsCollected = 1)
    {
        m_currentCollectedOrb += orbsCollected;

        FindObjectOfType<GameplayUIBehavior>().SetOrb(m_currentCollectedOrb);
    }

    public bool CanUnlockStatue()
    {
        return m_currentCollectedOrb > m_orbsNeedToUnlockStatue;
    }

    public void OnLoopCompleted()
    {
        var spawn = FindObjectOfType<SpawnManager>();

        if (CanUnlockStatue())
        {
            OnStatuesUnlocked();
            spawn.HandleNewLap(true);

        }
        else
        {
            spawn.HandleNewLap(false);

        }
        m_currentCollectedOrb = 0;
    }

    public void OnStatuesUnlocked()
    {

        //TODO: START STATUE UNLCOKED SEQUENCE HERE
        m_unlockedStates++;
        if(m_unlockedStates < paths.Length)
        GameObject.FindObjectOfType<Movement>().pathCreator = paths[m_unlockedStates];

        if (m_unlockedStates >3)
        {
            //TODO: START GAME FINISHED SEQUENCE HERE
            GameplayUIBehavior.Instance.YouWin();
            //Game completed 
        }
    }
}
