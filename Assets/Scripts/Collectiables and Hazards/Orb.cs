using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour
{
    [SerializeField] float scoreValue;

    private void OnTriggerEnter(Collider other)
    {
        var playerTag = other.gameObject.tag;
        if (playerTag == "Player" || playerTag == "Drill")
        {
            Debug.Log("Collected Orb");
            //TODO: insert Orb functionality here
            AkSoundEngine.PostEvent("PlayerPickup", this.gameObject);
            FindObjectOfType<GameplayUIBehavior>().setScore(scoreValue);
            ScoreManager.Instance.OnCollectibleCollected(ScoreManager.COLLECTIBLE_TYPE.ORBS);
            Destroy(this.gameObject);
        }
    }
}
