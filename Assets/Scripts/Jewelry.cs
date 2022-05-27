using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewelry : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        var playerTag = other.gameObject.tag;
        if (playerTag == "Player")
        {
            Debug.Log("Collected Jewelry");
            //TODO: insert Jewelry functionality here
            Destroy(this.gameObject);
        }
    }
}
