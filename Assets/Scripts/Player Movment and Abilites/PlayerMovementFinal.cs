using PathCreation;
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
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    float distanceTravelled;
    float yValue = 0f;


    //Boost Mechanic Variables
    [SerializeField][Range(1.0f, 5.0f)] private float _shiftSpeedBoost = 3.5f;
    private float startSpeedValue;
    private bool isSpeedBoostActive = false;


    void Start()
    {
        if (pathCreator != null)
        {
            startSpeedValue = Speed;
            playerBody = GetComponentInChildren<Rigidbody>();
            pathCreator.pathUpdated += OnPathChanged;
        }
    }
    // Update is called once per frame
    void Update()
    {

        PlayerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
        PlayerRotation = new Vector2(PlayerMovementInput.x, PlayerMovementInput.y).normalized;

        if(PlayerMovementInput.x > 0f)
        {

            Debug.Log("Where going right");
            distanceTravelled += Speed * Time.deltaTime;
        }
        else if(PlayerMovementInput.x < 0f)
        {
            Debug.Log("We're going left");
            distanceTravelled -= Speed * Time.deltaTime;
        }

        if(PlayerMovementInput.y > 0f) { yValue += Speed * Time.deltaTime; }
        else if(PlayerMovementInput.y < 0f) { yValue -= Speed * Time.deltaTime; }

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

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    private void MovePlayer()
    {
        playerBody.useGravity = PlayerMovementInput != Vector3.zero ? false : true;


        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        //playerBody.velocity = playerBody.useGravity ? -MoveVector.y : 0f;
        //playerBody.velocity = new Vector3(MoveVector.x, MoveVector.y, playerBody.velocity.z);
        playerBody.transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        playerBody.transform.position = new Vector3(playerBody.transform.position.x, yValue, playerBody.transform.position.z);
        //playerBody.velocity = new Vector3(playerBody.velocity.x, MoveVector.y, playerBody.velocity.z);


        if (playerBody.useGravity)
        {
            playerBody.AddForce(-Vector3.up * Sensitvity, ForceMode.Impulse);
            playerBody.rotation = Quaternion.Euler(0, 0, 0);
        }

    }
}
