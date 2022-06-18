using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    [SerializeField] int statueHealth = 20;
    private bool statueFree = false;

    // Start is called before the first frame update
    void Start()
    {
        AddBoxCollider();
        GameplayManager.Instance.setStatueIsFree(statueFree);
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
                    GameplayManager.Instance.setStatueIsFree(statueFree);
                    ChangeStatueColor();
                    AudioManager.instance.StatueSFX();
                }
                GameplayManager.Instance.resetOrbCount();
            }
        }
    }
    private void ChangeStatueColor()
    {
        //CHANGE COLOR OF STATUE
    }
}
