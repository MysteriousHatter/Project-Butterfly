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

        void Start()
        {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }

        void Update()
        {
            if (pathCreator != null)
            {
                if (Input.GetKey("up")){
                    print("up arrow key is held down");
                    yValue += speed * Time.deltaTime;
                }
                if (Input.GetKey("down")){
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
            Vector3 heading = transform.position - other.transform.position;
            float dot = Vector3.Dot(heading, other.transform.forward);
            tag = other.gameObject.tag;
            HurtPlayer(heading, dot, tag);
        }

        private void HurtPlayer(Vector3 heading, float dot, string tag)
        {
            Debug.Log("Collision detected");
            if (tag == "Hazard")
            {
                if (dot > 0)
                {
                    distanceTravelled -= speed * .3f;
                }
                else
                {
                    distanceTravelled += speed * .3f;
                }
            }
            //add interaction with score component when available
        }
        

    }
}
