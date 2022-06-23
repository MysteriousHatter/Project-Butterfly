using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

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
    [SerializeField] private Statue statueConfig;

    public int LinkCount { get; set; }
    [SerializeField] private float linkTimerPlaceholder = 3f;
     private float linkTimer; 

    private int m_unlockedStates = 0;
    public ShrineBehavior shrineBehavior;

    private bool nextPath;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        instance = this;
    }


    // Update is called once per frame
    void Update()
    {
        if (LinkCount > 0)
        {
            LinkTimeLeft();
        }
    }

    private void LinkTimeLeft()
    {
        linkTimer -= Time.deltaTime;
        if (linkTimer <= 0)
        {
            //1. Turn Of UI for link Counter
            GameplayUIBehavior.Instance.showLinkNumber(false);
            //2. Mutiply total linkCountScore by ten
            int totalLinkCOuntScore = LinkCount;
            //3. Update to scoreManager
            ScoreManager.Instance.OnLinkCollected(LinkCount);
            //4. Return LinkCount to zero
            LinkCount = 0;
        }
    }

    public void OnOrbCollected(int orbsCollected = 1)
    {
        m_currentCollectedOrb += orbsCollected;

    }

    public void OnLinkCollected(int linkCollected = 1)
    {
        LinkCount += linkCollected;
        if (LinkCount > 0)
        {
            linkTimer = linkTimerPlaceholder;
        }
        if (LinkCount >= 2)
        {
            //Show UI
            GameplayUIBehavior.Instance.showLinkNumber(true);

        }

    }



    public int getOrbCollected()
    {
        return m_currentCollectedOrb;
    }

    public void resetOrbCount()
    {
        GameplayUIBehavior.Instance.SetMaxOrb(m_currentCollectedOrb);
        m_currentCollectedOrb = 0;
    }

    public bool getStatueIsFree() { return nextPath; }
    public void setStatueIsFree(bool progress) { nextPath = progress; }

    public void OnLoopCompleted()
    {
        var spawn = FindObjectOfType<SpawnManager>();

        if (getStatueIsFree())
        {
            Debug.Log("Handle New lap");
            OnStatuesUnlocked();
            spawn.HandleNewLap(true);
            GameplayUIBehavior.Instance.StartTheGame();
            resetOrbCount();
            statueConfig.InstantiateToANewPostion();
            AudioManager.instance.LapClearedSFX();
        }
        else if(!getStatueIsFree())
        {
            Debug.Log("Don't handle a new lap");
            spawn.HandleNewLap(false);
            GameplayUIBehavior.Instance.OnSamePathRepeated();
        }

    }

    public void OnStatuesUnlocked()
    {

        //TODO: START STATUE UNLCOKED SEQUENCE HERE
        m_unlockedStates++;
        if(m_unlockedStates < paths.Length)
        GameObject.FindObjectOfType<Movement>().pathCreator = paths[m_unlockedStates];
        shrineBehavior.pathCreator = paths[m_unlockedStates];
        shrineBehavior.UpdatePath();
        ScoreManager.Instance.PathCompleted ((int)GameplayUIBehavior.Instance.TimeLeft);
        GameplayUIBehavior.Instance.SetOrb(0);
        if (m_unlockedStates >3)
        {
            //TODO: START GAME FINISHED SEQUENCE HERE
            GameplayUIBehavior.Instance.YouWin();
            FindObjectOfType<ScoreUI>()?.StartAnimation();
            AudioManager.instance.StageClearedSFX();
            //Game completed 
        }
    }
}
