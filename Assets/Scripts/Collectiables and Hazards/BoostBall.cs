using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBall : MonoBehaviour
{
    public GameObject collider;
    public GameObject player;

    void Start()
    {
        collider = GameObject.FindWithTag("Player");
        player = collider.transform.parent.gameObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        // the following two lines calculate if the other object is in front or behind the player
        // ensures that the other object is a BoostBall
        var playerTag = other.gameObject.tag;
        if (playerTag == "Player")
        {
            Debug.Log("Hit Hazard");
            AkSoundEngine.PostEvent("PlayerPickup", this.gameObject);
            player.GetComponent<Movement>().ActivateBoostBall = true;
        }
    }
}
