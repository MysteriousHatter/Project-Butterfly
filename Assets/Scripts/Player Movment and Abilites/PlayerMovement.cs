using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Vector3 PlayerMovementInput;
    private Vector2 PlayerRotation;

    //[SerializeField] private Transform PlayerCamera;
    [SerializeField] private Rigidbody playerBody;
    [Space]
    [SerializeField] private float Speed;
    [SerializeField] private float Sensitvity;



    void Start()
    {
        //playerBody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {

        PlayerMovementInput = new Vector3 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"),0f);
        PlayerRotation = new Vector2(PlayerMovementInput.x, PlayerMovementInput.y);
        MovePlayer();
        RotatePlayer();


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
        //playerBody.useGravity = PlayerMovementInput != Vector3.zero ? false : true;

        PlayerMovementInput.Normalize();
        transform.Translate(PlayerMovementInput * Speed * Time.deltaTime, Space.World);
        //playerBody.velocity = playerBody.useGravity ? -MoveVector.y : 0f;
        //playerBody.velocity = new Vector3(MoveVector.x, MoveVector.y, playerBody.velocity.z);

        //if (playerBody.useGravity)
        //{
        //    playerBody.AddForce(-Vector3.up * Sensitvity, ForceMode.Impulse);
        //}

    }


}
