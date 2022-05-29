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
            float playerInputAngle = Mathf.Atan2(PlayerRotation.y, PlayerRotation.x) * Mathf.Rad2Deg;
 
            // Set player rotation along with the path rotation
            Quaternion rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled);
            Vector3 angle = rotation.eulerAngles;
            // clear z to 0 since we don't need roll angles 
            angle.z = 0;
            rotation.eulerAngles = angle;
            playerBody.transform.rotation = rotation;

            // Use this to update angle around player models x (right) axis
            Vector3 right = playerBody.transform.right;
            right.y = 0;
            playerBody.transform.Rotate(new Vector3(-1, 0, 0), playerInputAngle - 90);
        }
    }




    private void MovePlayer()
    {
  
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput) * Speed;
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

    
}
