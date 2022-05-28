using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaTea : MonoBehaviour
{

 
    private void OnTriggerEnter(Collider other)
    {
        var playerTag = other.gameObject.tag;
        if (playerTag == "Player" || playerTag == "Drill")
        {
            Debug.Log("Collected Boba Tea");
            //TODO: insert Boba tea functionality here
            Destroy(this.gameObject);
        }
    }
}
