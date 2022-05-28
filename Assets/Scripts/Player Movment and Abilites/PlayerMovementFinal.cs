using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementFinal : MonoBehaviour
{
    
    //Movment variables
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerRotation;
    //[SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody playerBody;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitvity;


    //Boost Mechanic Variables
    [SerializeField][Range(1.0f, 5.0f)] private float _shiftSpeedBoost = 3.5f;
    private float startSpeedValue;
    private bool isSpeedBoostActive = false;


    void Start()
    {
        startSpeedValue = Speed;
        playerBody = GetComponentInChildren<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {

        PlayerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
        PlayerRotation = new Vector2(PlayerMovementInput.x, PlayerMovementInput.y).normalized;

        PlayerSpeedUp();

    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void PlayerSpeedUp()
    {
        if(Input.GetMouseButton(0))
        {
            this.gameObject.tag = "Drill";
            if(Speed < 20f)
            {
                Speed += _shiftSpeedBoost;
                //Speed = 20f;
            }
            else
            {
                Speed = 20f;
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            Speed = startSpeedValue;
            this.gameObject.tag = "Player";
        }
    }

    private void RotatePlayer()
    {
        if (PlayerRotation.sqrMagnitude != 0)
        {
            float joypos = Mathf.Atan2(PlayerRotation.x, PlayerRotation.y) * Mathf.Rad2Deg;

           playerBody.rotation = Quaternion.Euler(0, 0, -joypos);
        }
    }

    private void MovePlayer()
    {
        playerBody.useGravity = PlayerMovementInput != Vector3.zero ? false : true;


        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        //playerBody.velocity = playerBody.useGravity ? -MoveVector.y : 0f;
        playerBody.velocity = new Vector3(MoveVector.x, MoveVector.y, playerBody.velocity.z);

        if (playerBody.useGravity)
        {
            playerBody.AddForce(-Vector3.up * Sensitvity, ForceMode.Impulse);
            playerBody.rotation = Quaternion.Euler(0, 0, 0);
        }

    }
}
