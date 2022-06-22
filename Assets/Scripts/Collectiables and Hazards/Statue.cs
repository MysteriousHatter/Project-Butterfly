using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statue : MonoBehaviour
{
    [SerializeField] int statueHealth = 20;
    private bool statueFree = false;
    [SerializeField] GameObject FX;
    [SerializeField] Transform parent;

    [SerializeField] GameObject statueSignal;
    [SerializeField] Material freeStatue;

    // Start is called before the first frame update
    void Start()
    {
        //AddBoxCollider();
        GameplayUIBehavior.Instance.orbMax = statueHealth;
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
                    GameObject fx = Instantiate(FX, transform.position, Quaternion.identity);
                    fx.transform.parent = parent; 
                    //AudioManager.instance.StatueSFX();
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
