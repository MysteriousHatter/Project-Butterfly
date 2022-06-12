using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using System;

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
    [SerializeField] Paraloop_Mechanic paraloop;
    [SerializeField] float knockback = 1;

    // User this to lerp later
    private float currentRotationAngle = 0;

    public float TraveledDistance
    {
        get { return distanceTravelled; }
    }
    public float setTraveledDistance
    {
        set { distanceTravelled = value; }
    }
    [Space]
    // isVulnerable can be referenced by skill component to make player immune to damage and instead destroy other object
    public bool isInvulnerable;
    //Boost Mechanic Variables
    [SerializeField] [Range(1.0f, 5.0f)] private float _shiftSpeedBoost = 3.5f;
    private float startSpeedValue;
    private bool isSpeedBoostActive = false;
    [SerializeField] private float speedGauge;

    //Boost Ball
    public bool ActivateBoostBall { get; set; }
    [SerializeField] private float boostBallSpeed;
    [SerializeField] private float boostBallTime;
    private Vector3 velocity = Vector3.zero;
    void Start()
    {
        if (pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            pathCreator.pathUpdated += OnPathChanged;
            startSpeedValue = Speed;
            playerBody = GetComponentInChildren<Rigidbody>();
            isInvulnerable = false;
            OnPathChanged();
            setBoostRefill(90f);
            paraloop = GetComponentInChildren<Paraloop_Mechanic>();
            ActivateBoostBall = false;
        }

        if(knockback == 0)
        {
            //Since default knock back value should never be 0
            knockback = 0.3f;
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
            MoveForward();

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

        Quaternion rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
        Vector3 angle = rotation.eulerAngles;
        angle.z = 0;
        rotation.eulerAngles = angle;
        playerBody.transform.rotation = rotation;
    }


    private void PlayerSpeedUp()
    {
        if (Input.GetMouseButton(0) && speedGauge > 0)
        {
            speedGauge--;
            this.gameObject.tag = "Drill";
            paraloop.InstantiateTransformations(false);
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
            paraloop.InstantiateTransformations(true);
        }
        else
        {
            Speed = startSpeedValue;
            this.gameObject.tag = "Player";
            paraloop.InstantiateTransformations(true);
        }
    }


    private void RotatePlayer()
    {
        if (PlayerRotation.sqrMagnitude != 0)
        {
            float playerInputAngle = Mathf.Atan2(PlayerRotation.y, PlayerRotation.x) * Mathf.Rad2Deg;
 
            // Set player rotation along with the path rotation
            Quaternion rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
            Vector3 angle = rotation.eulerAngles;
            // clear z to 0 since we don't need roll angles 
            angle.z = 0;
            rotation.eulerAngles = angle;
            playerBody.transform.rotation = rotation;

             float rotateSpeed = 360f;
            float angleSnapAtDegree = rotateSpeed * Time.deltaTime;

            float AngleDifference = 0f;
            if (playerInputAngle * currentRotationAngle < 0 && Mathf.Abs(playerInputAngle - currentRotationAngle) != 180f)
            {
                if (Mathf.Abs(playerInputAngle - currentRotationAngle) > 180f)
                {
                    if (180f - Mathf.Abs(playerInputAngle) + 180f - Mathf.Abs(currentRotationAngle) < 90f)
                    {
                        AngleDifference = 180f - Mathf.Abs(playerInputAngle) + 180f - Mathf.Abs(currentRotationAngle);
                    }
                    else
                    {
                        AngleDifference = playerInputAngle + currentRotationAngle + 90f;
                    }
                }
            }
            else
            {
                AngleDifference = playerInputAngle - currentRotationAngle;
            }
            if(Mathf.Abs(AngleDifference) <= angleSnapAtDegree * 1.1F)
            {
                currentRotationAngle = playerInputAngle;
            }else
            if (ShouldTurnLeft(playerInputAngle, currentRotationAngle))
            {
                currentRotationAngle += rotateSpeed * Time.deltaTime;

            }
            else
            {
                currentRotationAngle -= rotateSpeed * Time.deltaTime;
            }
            if(currentRotationAngle > 180F)
            {
                currentRotationAngle =  currentRotationAngle - 360F;
            }
            if(currentRotationAngle < -180F)
            {
                currentRotationAngle = currentRotationAngle + 360F;
            }
            // Use this to update angle around player models x (right) axis
            Vector3 right = playerBody.transform.right;
            right.y = 0;
            playerBody.transform.Rotate(new Vector3(-1, 0, 0), currentRotationAngle - 90);
        }
    }


    private bool ShouldTurnLeft(float targetAngle, float currentAngle)
    {
        bool result = false;

        Vector2 currentAngleVecetor = new Vector2(Mathf.Cos(currentAngle * Mathf.Deg2Rad), Mathf.Sin(currentAngle * Mathf.Deg2Rad));
        Vector2 leftOfCurrentAngleVector = new Vector2(-currentAngleVecetor.y, currentAngleVecetor.x);

        Vector2 currentTargetvector = new Vector2(Mathf.Cos(targetAngle * Mathf.Deg2Rad), Mathf.Sin(targetAngle * Mathf.Deg2Rad));

        if (Vector2.Dot(currentTargetvector,leftOfCurrentAngleVector) > 0)
        {
            result = true;
        }
        return result;
    }

    private void MovePlayer()
    {
        //TODO: Stop delay with spawning points
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        //paraloop.InstantiateTransformations();
        if (MoveVector.sqrMagnitude != 0)
        {
            Debug.Log("Start points");
            
        }
        else
        {
            paraloop.ClearNeighbors();
        }

        playerBody.transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        playerBody.transform.position = new Vector3(playerBody.transform.position.x, yValue, playerBody.transform.position.z);

    }

    public float getBoostRefill() { return speedGauge; }
    public void setBoostRefill(float boostRefill) 
    {
        if(speedGauge < 90)
        {
            speedGauge += boostRefill;
        }
        else
        {
            speedGauge = 90f;
        }

    } 

    public void MoveBack(Vector3 moveDirection)
    {
        float pushedBackDistance =Vector3.Dot( pathCreator.path.GetDirectionAtDistance(distanceTravelled), moveDirection);
        if (pushedBackDistance < 0)
        {
            distanceTravelled -= Speed * knockback;
        }
        else if(pushedBackDistance > 0)
        {
            distanceTravelled += Speed * knockback;
        }
        if(pushedBackDistance != 0)
        {
            playerBody.transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
            playerBody.transform.position = new Vector3(playerBody.transform.position.x, yValue, playerBody.transform.position.z);
        }
    }

    //TODO: Fix Roation to face right when dashing
    public void MoveForward()
    {
        if (ActivateBoostBall)
        {
            StartCoroutine(FindObjectOfType<CameraMovement>().SpeedUpCamera(0.8f));
            distanceTravelled += boostBallSpeed * 0.3f;
            playerBody.transform.position = Vector3.SmoothDamp(playerBody.transform.position, pathCreator.path.GetDirectionAtDistance(distanceTravelled), ref velocity, boostBallTime);
            ActivateBoostBall = false;
        }
        //playerBody.transform.position = new Vector3(playerBody.transform.position.x, yValue, playerBody.transform.position.z);

    }
}

    

