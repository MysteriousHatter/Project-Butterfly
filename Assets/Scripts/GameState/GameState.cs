using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static new GameState Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameState();
            }
            return instance;
        }
    }

    private static GameState instance;

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
    }

    public bool CanUnlockStatue()
    {
        return m_currentCollectedOrb > m_orbsNeedToUnlockStatue;
    }

    public void OnLoopCompleted()
    {
        if (CanUnlockStatue())
        {
            OnStatuesUnlocked();
        }
        m_currentCollectedOrb = 0;
    }

    public void OnStatuesUnlocked()
    {

        //TODO: START STATUE UNLCOKED SEQUENCE HERE
        m_unlockedStates++;
        if (m_unlockedStates >=3)
        {
            //TODO: START GAME FINISHED SEQUENCE HERE
            //Game completed 
        }
    }
}
