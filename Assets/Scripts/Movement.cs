using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PathCreation
{
    public class Movement : MonoBehaviour
    {

        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
        float distanceTravelled;
        float yValue = 0;
        // isVulnerable can be referenced by skill component to make player immune to damage and instead destroy other object
        public bool isInvulnerable;
        void Start()
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
                isInvulnerable = false;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                if (Input.GetKey("up"))
                {
                    print("up arrow key is held down");
                    yValue += speed * Time.deltaTime;
                }
                if (Input.GetKey("down"))
                {
                    print("move down");
                    yValue -= speed * Time.deltaTime;
                }
                if (Input.GetKey("left"))
                {
                    print("move left");
                    distanceTravelled -= speed * Time.deltaTime;
                }
                if (Input.GetKey("right"))
                {
                    print("move right");
                    distanceTravelled += speed * Time.deltaTime;
                }
                // apply left right 
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
                // apply up down
                transform.position = new Vector3(transform.position.x, yValue, transform.position.z);

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
            tag = other.gameObject.tag;
            if (tag == "Hazard")
            {
                HurtPlayer(other, heading, dot);
            }

        }


        private void HurtPlayer(Collision other, Vector3 heading, float dot)
        {
            Debug.Log("Collision detected, hazard hit");
            if (isInvulnerable)
            {
                Destroy(other.gameObject);
            }

            else
            {
                //if dot is greater than 0, that means the object is facing the player, so head-on collision
                if (dot > 0)
                {
                    //this will move the player a set distance on the path away from current location
                    distanceTravelled -= speed * .3f;
                }
                //if dot is less than 0, that means the object is facing away from the player, so collision from behind
                else
                {
                    distanceTravelled += speed * .3f;
                }
            }
            //add interaction with score and time component when available
        }


    }
}
