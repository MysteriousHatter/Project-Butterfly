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
        if (playerTag == "Player" || playerTag == "Drill" || playerTag == "Vortex")
        {
            Debug.Log("Collected Boba Tea");
            //TODO: insert Boba functionality here
            FindObjectOfType<Movement>().setBoostRefill(boostRefill);
            ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.BOBA);

            Destroy(this.gameObject);
        }
    }
}
