using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public enum COLLECTIBLE_TYPE
    {
        BOBA,
        ORBS,
        RING,
        PURPLEORB,
        JEWELRY,
        STATUE
    }

    static Dictionary<COLLECTIBLE_TYPE, int> SCORE_VALUE = new Dictionary<COLLECTIBLE_TYPE, int>()
    {
        [COLLECTIBLE_TYPE.BOBA] = 50,
        [COLLECTIBLE_TYPE.ORBS] = 10,
        [COLLECTIBLE_TYPE.RING] = 10,
        [COLLECTIBLE_TYPE.PURPLEORB] = 100,
        [COLLECTIBLE_TYPE.JEWELRY] = 200,
        [COLLECTIBLE_TYPE.STATUE] = 500,

    };
    public struct PathCompletionRecord
    {
        public int orbsCollected;
        public int remainingTime;
        public int bobaTeaCollected;
        public int ringCollected;
        public int purpleOrbsCollected;
        public int jewelryCollected;
        public bool StateUnlocked;

        public int Score()
        {
            int result = 0;
            result += orbsCollected * SCORE_VALUE[COLLECTIBLE_TYPE.ORBS];
            result += bobaTeaCollected * SCORE_VALUE[COLLECTIBLE_TYPE.BOBA];
            result += ringCollected * SCORE_VALUE[COLLECTIBLE_TYPE.RING];
            result += purpleOrbsCollected * SCORE_VALUE[COLLECTIBLE_TYPE.PURPLEORB];
            result += jewelryCollected * SCORE_VALUE[COLLECTIBLE_TYPE.JEWELRY];
            result += GetTimeBonus();
            return result;
        }

        private int GetTimeBonus()
        {
            //TODO: time value calculation need clarify
            return 1000 - remainingTime; 
        }
    }
    public static ScoreManager Instance
    {
        get
        {
            instance = GameObject.FindObjectOfType<ScoreManager>();
            if (instance == null)
            {

                GameObject a = new GameObject("a");
                a.AddComponent<ScoreManager>();
                instance = a.GetComponent<ScoreManager>();

            }
            return instance;
        }
    }

    private static ScoreManager instance;

    private PathCompletionRecord currentRunRecord;
    List<PathCompletionRecord> allRunRecords;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnStatueUnlocked()
    {

    }


    public void Fail()
    {

    }


    public void PathCompleted()
    {

    }

    public void RestartPath()
    {

    }

    public void OnCollectibleCollected(COLLECTIBLE_TYPE type)
    {
        switch (type)
        {
            case COLLECTIBLE_TYPE.ORBS:
                currentRunRecord.orbsCollected++;
                break;
            case COLLECTIBLE_TYPE.BOBA:
                currentRunRecord.bobaTeaCollected++;
                break;
            case COLLECTIBLE_TYPE.RING:
                currentRunRecord.ringCollected++;
                break;
            case COLLECTIBLE_TYPE.PURPLEORB:
                currentRunRecord.purpleOrbsCollected++;
                break;
            case COLLECTIBLE_TYPE.JEWELRY:
                currentRunRecord.jewelryCollected++;
                break;
            case COLLECTIBLE_TYPE.STATUE:
                currentRunRecord.StateUnlocked = true;
                break;

        }
    }
}
