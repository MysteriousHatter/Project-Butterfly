using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class ChargerAI : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    

    [Task]
    bool IsEnemyVisable()
    {
        if (Vector3.Distance(player.transform.position,transform.position) < 10f){
            return true;
        }
        else { 
            return false; 
        }
    }
    [Task]
    void AimAt_Target() {

        Task.current.Succeed();
    }
}
