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
    public float deltaY = 0f;
    public float Speed = 5;
    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private float Sensitvity;
    [Space]
    //Path variables
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    float distanceTravelled;
    [SerializeField] Paraloop_Mechanic paraloop;
    [SerializeField] float knockback = 0.3f;
    [SerializeField] Animator playerAnimation;

    // User this to lerp later
    private float currentRotationAngle = 0;

    //Animator Script
    Animator myAnimator;

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
    [SerializeField][Range(1.0f, 50.0f)] private float _shiftSpeedBoost = 50f;
    private float startSpeedValue;
    private bool isSpeedBoostActive = false;
    [SerializeField] private float speedGauge;
    [SerializeField] BoostGauge boostGauge;

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
            setBoostRefill(300f);
            paraloop = GetComponentInChildren<Paraloop_Mechanic>();
            myAnimator = GetComponentInChildren<Animator>();
            ActivateBoostBall = false;
            boostGauge.SetMaxBoost(speedGauge);
        }

        if (knockback == 0)
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

            if (ActivateBoostBall)
            {
                PlayerMovementInput = Vector3.right;
                PlayerRotation = new Vector2(PlayerMovementInput.x, 0);
            }
            else
            {
                //if (PlayerMovementInput.y > 0f) { yValue += Speed * Time.deltaTime; }
                //else if (PlayerMovementInput.y < 0f) { yValue -= Speed * Time.deltaTime; }
            }
            if (stunTime > 0)
                return;
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
            MoveForward();


        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        RotatePlayer();
        AnimationUpdate();
    }

    private void AnimationUpdate()
    {
        playerAnimation.SetBool("Moving", PlayerMovementInput.sqrMagnitude != 0);

        playerAnimation.SetBool("Drilling", Input.GetMouseButton(0) && speedGauge > 0);

    }
    private void CheckIfPlayerIsInvinisable()
    {
        if (playerBody.gameObject.tag == "Drill")
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
        distanceTravelled = 0;

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
            boostGauge.SetBoost(speedGauge);
            //this.gameObject.tag = "Drill";
            playerBody.gameObject.tag = "Drill";
            myAnimator.gameObject.tag = "Drill";
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
            //this.gameObject.tag = "Player";
            playerBody.gameObject.tag = "Player";
            myAnimator.gameObject.tag = "Player";
            paraloop.InstantiateTransformations(true);
        }
        else
        {
            Speed = startSpeedValue;
            //this.gameObject.tag = "Player";
            playerBody.gameObject.tag = "Player";
            myAnimator.gameObject.tag = "Player";
            paraloop.InstantiateTransformations(true);
        }
    }


    private float idleWaitLength = 0f;
    private void RotatePlayer()
    {

           float rotateSpeed = 360f;
        Vector2 localPlayerRotation = PlayerRotation;
        if (currentRotationAngle < 90f && currentRotationAngle > 0f)
        {
            Debug.Log("Bad Area");
        }

        float idleAngleOffset = 0f;
            if(PlayerRotation.sqrMagnitude  == 0  && stunTime <= 0 )
            {
            //Pretend moving up straight so kaya is upright
            localPlayerRotation.y = 1;
            localPlayerRotation.x = 0;
            // this offset is needed since the idle animation has a 90 degree offset comparing to other
                idleAngleOffset = +Mathf.Lerp(0, 90, 1f);
                rotateSpeed = 360f;
                idleWaitLength += Time.deltaTime;
        }
        else
        {
            //reset idle timer when player presses anything
            idleWaitLength = 0f;
        }
            float playerInputAngle = Mathf.Atan2(localPlayerRotation.y, localPlayerRotation.x) * Mathf.Rad2Deg;

                // Set player rotation along with the path rotation
            Quaternion rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
            Vector3 angle = rotation.eulerAngles;
            // clear z to 0 since we don't need roll angles 
            angle.z = 0;
            rotation.eulerAngles = angle;
            playerBody.transform.rotation = rotation;

            float angleSnapAtDegree = rotateSpeed * Time.deltaTime;

            float AngleDifference = 0f;
            if (playerInputAngle * (currentRotationAngle ) < 0 && Mathf.Abs(playerInputAngle - (currentRotationAngle ) )!= 180f)
            {
                if (Mathf.Abs(playerInputAngle - currentRotationAngle) > 180f)
                {
                    if (180f - Mathf.Abs(playerInputAngle) + 180f - Mathf.Abs(currentRotationAngle) < 90f)
                    {
                        AngleDifference = 180f - Mathf.Abs(playerInputAngle) + 180f - Mathf.Abs(currentRotationAngle);
                    }
                    else
                    {
                        AngleDifference = playerInputAngle + currentRotationAngle + 180f;
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
            if (ShouldTurnLeft(playerInputAngle, currentRotationAngle ))
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
            if (playerAnimation.GetCurrentAnimatorClipInfo(0)[0].clip.name.Contains("injured"))
            {
                currentRotationAngle = 0f;
            }

        playerBody.transform.Rotate(new Vector3(-1, 0, 0), currentRotationAngle - 90 - idleAngleOffset);
        
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

        if(stunTime > 0)
        {
            stunTime -= Time.deltaTime;
            return;
        }
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
        //paraloop.InstantiateTransformations()
        if (MoveVector.sqrMagnitude != 0)
        {
            myAnimator.SetBool("Moving", true);
            PlayerSpeedUp();

        }
        else
        {
            paraloop.ClearNeighbors();
            myAnimator.SetBool("Moving", false);
        }

        playerBody.transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        if(PlayerMovementInput.y > 0)
        {
            deltaY +=  Speed * Time.deltaTime;
        }
        else if(PlayerMovementInput.y < 0)
        {
            deltaY -= Speed * Time.deltaTime;
        }
    
        playerBody.transform.position = new Vector3(playerBody.transform.position.x, deltaY, playerBody.transform.position.z);

    }

    public float getBoostRefill() { return speedGauge; }
    public void setBoostRefill(float boostRefill)
    {
        if (speedGauge < 300)
        {
            speedGauge += boostRefill;
            boostGauge.SetBoost(speedGauge);
        }
        else
        {
            speedGauge = 300f;
            boostGauge.SetBoost(speedGauge);
        }

    }

    float stunTime = 0f;
    public void MoveBack(Vector3 moveDirection)
    {
       // Stun animation plays for 1,16 seconds so we don't let player move in that duration
        stunTime = 1.16f;
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
            playerBody.transform.position = new Vector3(playerBody.transform.position.x, deltaY, playerBody.transform.position.z);
        }
        playerAnimation.SetTrigger("Damage");
        currentRotationAngle = 0f;
    }

    //TODO: Fix Roation to face right when dashing

    public IEnumerator MoveForward()
    {
        while (ActivateBoostBall)
        {
            Speed = 20f;
            PlayerMovementInput = Vector3.right;
            yield return new WaitForSeconds(1.0f);
            ActivateBoostBall = false;
        }

    }


    //public void MoveForward()
    //{
    //    if (ActivateBoostBall)
    //    {
    //        Speed = 20f;
    //        PlayerMovementInput = Vector3.right;
    //        //StartCoroutine(FindObjectOfType<CameraMovement>().SpeedUpCamera(0.8f));
    //        //distanceTravelled += boostBallSpeed * 0.3f;
    //        //playerBody.transform.position = Vector3.SmoothDamp(playerBody.transform.position, pathCreator.path.GetDirectionAtDistance(distanceTravelled), ref velocity, boostBallTime);
    //        //ActivateBoostBall = false;
    //    }
    //    //playerBody.transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled);
    //    //playerBody.transform.position = new Vector3(playerBody.transform.position.x, yValue, playerBody.transform.position.z);

    //}
}

    

