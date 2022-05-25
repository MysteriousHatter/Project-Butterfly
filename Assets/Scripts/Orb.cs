using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        var playerTag = other.gameObject.tag;
        if (playerTag == "Player")
        {
            Debug.Log("Collected Orb");
            //TODO: insert Orb functionality here
            Destroy(this.gameObject);
        }
    }
}
