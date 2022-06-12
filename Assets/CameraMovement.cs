using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float CameraArmLength = 5f;
    public GameObject player;
    public GameObject cameraRightObject;
    private Vector3 cameraSpeedHolder;
    private bool isBoosting = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isBoosting)
        {
            // always be on the right side of the player transform
            Vector3 cancelYOffeset = cameraRightObject.transform.right;
            cancelYOffeset.y = 0;
            this.gameObject.transform.position = cancelYOffeset * CameraArmLength + player.transform.position;
            this.gameObject.transform.LookAt(player.transform.position);
        }
    }

    public IEnumerator SpeedUpCamera(float delay)
    {
        isBoosting = true;
        cameraSpeedHolder = this.transform.position;
        float cameraLerp = 0;


        while (cameraLerp < delay)
        {
            Vector3 cancelYOffeset = cameraRightObject.transform.right;
            cancelYOffeset.y = 0;
            cameraLerp += Time.deltaTime;
            float lerp = cameraLerp / delay;

            this.gameObject.transform.position = Vector3.Lerp(cameraSpeedHolder, player.transform.position + cancelYOffeset * 10f, lerp);
            this.gameObject.transform.LookAt(player.transform.position);

           
            yield return null;
        }
        isBoosting = false;

    }
}
