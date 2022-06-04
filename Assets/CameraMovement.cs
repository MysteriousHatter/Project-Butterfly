using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;
    public GameObject cameraRightObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // always be on the right side of the player transform
        Vector3 cancelYOffeset = cameraRightObject.transform.right;
        cancelYOffeset.y = 0;
        this.gameObject.transform.position = cancelYOffeset * 10f + player.transform.position;
        this.gameObject.transform.LookAt(player.transform.position);
    }




}
