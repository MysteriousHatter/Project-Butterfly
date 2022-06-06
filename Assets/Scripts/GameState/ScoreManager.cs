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
        JEWELRY,
        STATUE
    }

    static Dictionary<COLLECTIBLE_TYPE, int> SCORE_VALUE = new Dictionary<COLLECTIBLE_TYPE, int>()
    {
        [COLLECTIBLE_TYPE.BOBA] = 50,
        [COLLECTIBLE_TYPE.RING] = 10,
        [COLLECTIBLE_TYPE.ORBS] = 100,
        [COLLECTIBLE_TYPE.JEWELRY] = 200,
        [COLLECTIBLE_TYPE.STATUE] = 500,

    };
    public struct PathCompletionRecord
    {
        public int orbsCollected ;
        public int remainingTime  ;
        public int bobaTeaCollected ;
        public int ringCollected  ;
        public int jewelryCollected ;
        public int StateUnlocked ;

        public int Score()
        {
            int result = 0;
            result += orbsCollected * SCORE_VALUE[COLLECTIBLE_TYPE.ORBS];
            result += bobaTeaCollected * SCORE_VALUE[COLLECTIBLE_TYPE.BOBA];
            result += ringCollected * SCORE_VALUE[COLLECTIBLE_TYPE.RING];
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
    List<PathCompletionRecord> allRunRecords = new List<PathCompletionRecord>();


    void Start()
    {
        currentRunRecord = new PathCompletionRecord();

    }


    public void Fail()
    {
        // erase all record since player failed

        currentRunRecord = new PathCompletionRecord();
        allRunRecords.Clear();
    }


    public void PathCompleted(int timeLeft)
    {
        // add current record, and new record for next path
        currentRunRecord.remainingTime = timeLeft;
        allRunRecords.Add(currentRunRecord);
        currentRunRecord = new PathCompletionRecord();
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
            case COLLECTIBLE_TYPE.JEWELRY:
                currentRunRecord.jewelryCollected++;
                break;
            case COLLECTIBLE_TYPE.STATUE:
                currentRunRecord.StateUnlocked = 1;
                break;

        }
    }

    public PathCompletionRecord GetSumary()
    {
        PathCompletionRecord record = new PathCompletionRecord();


        for(int i = 0; i < allRunRecords.Count; i++)
        {
            record.jewelryCollected += allRunRecords[i].jewelryCollected;
            record.orbsCollected += allRunRecords[i].orbsCollected;
            record.ringCollected += allRunRecords[i].ringCollected;
            record.jewelryCollected += allRunRecords[i].jewelryCollected;
            record.StateUnlocked += allRunRecords[i].StateUnlocked;
            record.bobaTeaCollected += allRunRecords[i].bobaTeaCollected;

            if (i == allRunRecords.Count - 1)
            {
                record.remainingTime = allRunRecords[i].remainingTime;
            }
        }
        return record;
    }
}
