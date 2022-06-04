using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookUpUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    Movement playerMovementScript;
    void Start()
    {
        playerMovementScript = GameObject.FindObjectOfType<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
       this.gameObject.transform.position =  playerMovementScript.pathCreator.path.GetPointAtDistance(playerMovementScript.TraveledDistance);
        this.gameObject.transform.rotation = playerMovementScript.pathCreator.path.GetRotationAtDistance(playerMovementScript.TraveledDistance);
            // find the path
            // get players xz position
            // have this game object face the driection of the curve
    }
}
