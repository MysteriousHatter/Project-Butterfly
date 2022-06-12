using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField] float scoreValue;

    private void OnTriggerEnter(Collider other)
    {
        var playerTag = other.gameObject.tag;
        if (playerTag == "Player" || playerTag == "Drill" || playerTag == "Vortex")
        {
            Debug.Log("Collected Orb");
            //TODO: insert Orb functionality here
            FindObjectOfType<GameplayUIBehavior>().setScore(scoreValue);
            GameplayManager.Instance.OnOrbCollected();
            //TODO: Update statue count in UI and gameManager look at chris' code
            Destroy(this.gameObject);
        }
    }
}
