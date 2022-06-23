using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    int statueHealth;
    private bool statueFree = false;
    [SerializeField] GameObject FX;
    [SerializeField] Transform parent;

    [SerializeField] GameObject statueSignal;
    [SerializeField] Material freeStatue;
    [SerializeField] Material lockedStatue;

    [SerializeField] public StatueBehaviorConfig[] config;
    int lapCount = -1;

    // Start is called before the first frame update
    void Start()
    {
        //AddBoxCollider();
        InstantiateToANewPostion();
        //GameplayUIBehavior.Instance.orbMax = statueHealth;
        //GameplayManager.Instance.setStatueIsFree(statueFree);
    }

    public void InstantiateToANewPostion()
    {
        lapCount++;
        if(lapCount < config.Length)
        {
            statueHealth = config[lapCount].health;
            GameplayUIBehavior.Instance.orbMax = statueHealth;
            GameplayManager.Instance.setStatueIsFree(false);
            this.gameObject.transform.position = config[lapCount].SpawnLocation.transform.position;
            this.gameObject.transform.rotation = config[lapCount].SpawnLocation.transform.rotation;
            statueSignal.GetComponent<MeshRenderer>().material = lockedStatue;
        }
    }

    private void AddBoxCollider()
    {
       Collider statueCollison = this.gameObject.AddComponent<BoxCollider>();
        statueCollison.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        var playerTag = other.gameObject.tag;
        if (playerTag == "Player" || playerTag == "Drill")
        {
            if (GameplayManager.Instance.getOrbCollected() > 0 && !statueFree)
            {
                Debug.Log("Decrese Statue Health");
                statueHealth -= GameplayManager.Instance.getOrbCollected();
                if(statueHealth <= 0)
                {
                    statueFree = true;
                    GameplayManager.Instance.setStatueIsFree(true);
                    ChangeStatueColor();
                    GameObject fx = Instantiate(FX, transform.position, Quaternion.identity);
                    fx.transform.parent = parent; 
                    AudioManager.instance.StatueSFX();
                }
                GameplayManager.Instance.resetOrbCount();
            }
        }
    }
    private void ChangeStatueColor()
    {
        //CHANGE COLOR OF STATUE
        statueSignal.GetComponent<MeshRenderer>().material = freeStatue;
        
    }
}
