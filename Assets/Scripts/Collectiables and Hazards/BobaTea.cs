using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobaTea : MonoBehaviour
{
    public float boostRefill;
    [SerializeField] private float scoreValue = 50f;
 
    private void OnTriggerEnter(Collider other)
    {
        var playerTag = other.gameObject.tag;
        if (playerTag == "Player" || playerTag == "Drill")
        {
            Debug.Log("Collected Boba Tea");
            //TODO: insert Boba functionality here
            AkSoundEngine.PostEvent("PlayerPickup", this.gameObject);
            FindObjectOfType<GameplayUIBehavior>().setScore(scoreValue);
            FindObjectOfType<Movement>().setBoostRefill(boostRefill);
            Destroy(this.gameObject);
        }
    }
}
