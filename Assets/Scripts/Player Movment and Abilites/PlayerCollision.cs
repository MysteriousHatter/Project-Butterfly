using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    Movement playerMovement;
    [SerializeField] float setBackSpeed = 5f;

    private void Start()
    {
        playerMovement = GetComponent<Movement>();
    }

    private void OnCollisionEnter(Collision other)
    {
        // the following two lines calculate if the other object is in front or behind the player
        Vector3 heading = transform.position - other.transform.position;
        float dot = Vector3.Dot(heading, other.transform.forward);
        // ensures that the other object is a hazard
        var playerTag = other.gameObject.tag;
        if (playerTag == "Hazard")
        {
            HurtPlayer(other, heading, dot);
        }

    }

    private void HurtPlayer(Collision other, Vector3 heading, float dot)
    {
        Debug.Log("Collision detected, hazard hit");
        AkSoundEngine.PostEvent("PlayerHurt", this.gameObject);
        if (playerMovement.isInvulnerable)
        {
            Destroy(other.gameObject);
            //TODO: add positive interaction when available
        }

        else
        {
            //if dot is greater than 0, that means the object is facing the player, so head-on collision
            if (dot > 0)
            {
                float distanceTraveled = playerMovement.TraveledDistance;
                //this will move the player a set distance on the path away from current location
                distanceTraveled -= setBackSpeed * .3f;
                playerMovement.setTraveledDistance = distanceTraveled;
            }
            //if dot is less than 0, that means the object is facing away from the player, so collision from behind
            else
            {
                float distanceTraveled = playerMovement.TraveledDistance;
                //this will move the player a set distance on the path away from current location
                distanceTraveled += setBackSpeed * .3f;
                playerMovement.setTraveledDistance = distanceTraveled;
            }
        }
        //TODO: add negative interaction with score and time component when available
    }
}
