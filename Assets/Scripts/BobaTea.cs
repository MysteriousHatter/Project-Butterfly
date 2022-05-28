using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaTea : MonoBehaviour
{
    public float boostRefill;
 
    private void OnTriggerEnter(Collider other)
    {
        var playerTag = other.gameObject.tag;
        if (playerTag == "Player" || playerTag == "Drill")
        {
            Debug.Log("Collected Boba Tea");
            //TODO: insert Boba functionality here
            //TODO: Upate Score and UI Manager to update the score
            //TODO: Update boost metter in Movement script
            Destroy(this.gameObject);
        }
    }
}
