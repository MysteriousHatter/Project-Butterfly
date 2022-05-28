using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class Movement : MonoBehaviour
{

    //Movment variables
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerRotation;
    float yValue = 0;
    public float Speed = 5;
    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private float Sensitvity;
    [Space]
    //Path variables
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    float distanceTravelled;
    public float TraveledDistance
    {
        get { return distanceTravelled; }
    }
    [Space]
    // isVulnerable can be referenced by skill component to make player immune to damage and instead destroy other object
    public bool isInvulnerable;
    //Boost Mechanic Variables
    [SerializeField] [Range(1.0f, 5.0f)] private float _shiftSpeedBoost = 3.5f;
    private float startSpeedValue;
    private bool isSpeedBoostActive = false;
    [SerializeField] private float speedGauge;
    void Start()
    {
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
            startSpeedValue = Speed;
            playerBody = GetComponentInChildren<Rigidbody>();
            isInvulnerable = false;
        }
    }

    void Update()
    {
        CheckIfPlayerIsInvinisable();

        if (pathCreator != null)
        {
            //TODO: Add Kaya VFX trail
            PlayerMovementInput = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);
            PlayerRotation = new Vector2(PlayerMovementInput.x, PlayerMovementInput.y).normalized;

            if (PlayerMovementInput.x > 0f)
            {

                Debug.Log("Where going right");
                distanceTravelled += Speed * Time.deltaTime;
            }
            else if (PlayerMovementInput.x < 0f)
            {
                Debug.Log("We're going left");
                distanceTravelled -= Speed * Time.deltaTime;
            }

            if (PlayerMovementInput.y > 0f) { yValue += Speed * Time.deltaTime; }
            else if (PlayerMovementInput.y < 0f) { yValue -= Speed * Time.deltaTime; }

            PlayerSpeedUp();

        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
    }

    private void CheckIfPlayerIsInvinisable()
    {
        if (this.gameObject.tag == "Drill")
        {
            isInvulnerable = true;
        }
        else
        {
            isInvulnerable = false;
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
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

    private void PlayerSpeedUp()
    {
        if (Input.GetMouseButton(0) && speedGauge > 0)
        {
            speedGauge--;
            this.gameObject.tag = "Drill";
            if (Speed < 20f)
            {
                Speed += _shiftSpeedBoost;
                //Speed = 20f;
            }
            else
            {
                Speed = 20f;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Speed = startSpeedValue;
            this.gameObject.tag = "Player";
        }
        else
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


    private void HurtPlayer(Collision other, Vector3 heading, float dot)
    {
        Debug.Log("Collision detected, hazard hit");
        if (isInvulnerable)
        {
            Destroy(other.gameObject);
            //TODO: add positive interaction when available
        }

        else
        {
            //if dot is greater than 0, that means the object is facing the player, so head-on collision
            if (dot > 0)
            {
                //this will move the player a set distance on the path away from current location
                distanceTravelled -= Speed * .3f;
            }
            //if dot is less than 0, that means the object is facing away from the player, so collision from behind
            else
            {
                distanceTravelled += Speed * .3f;
            }
        }
        //TODO: add negative interaction with score and time component when available
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
