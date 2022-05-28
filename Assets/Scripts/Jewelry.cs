using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jewelry : MonoBehaviour
{

    [SerializeField] int scoreValue;
    private void OnTriggerEnter(Collider other)
    {
        var playerTag = other.gameObject.tag;
        if (playerTag == "Player" || playerTag == "Drill")
        {
            Debug.Log("Collected Jewelry");
            //TODO: insert Jewelry functionality here
            //TODO: Upate Score and UI Manager to update the score
            Destroy(this.gameObject);
        }
    }
}
