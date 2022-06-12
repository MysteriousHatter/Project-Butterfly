using UnityEngine;
using System.Collections;



namespace Panda.Examples.Shooter
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 1.0f;
        public float duration = 15.0f;
        public float damage = 1.0f;
        public GameObject impactPrefab;

        public GameObject shooter;
        float startTime;

        // Use this for initialization
        void Start()
        {
            
            startTime = Time.time;
        }

        void FixedUpdate()
        {
            Vector3 velocity = this.transform.forward * speed;
            this.transform.position = this.transform.position + velocity * Time.deltaTime;

            if (Time.time - startTime > duration)
                Destroy(this.gameObject);
            else
                BulletScan();
                
        }

        void BulletScan()
        {

            RaycastHit hit;
            var ray = new Ray(this.transform.position, this.transform.forward);
            if (Physics.Raycast(ray, out hit, 1.0f))
            {
                var other = hit.collider;

            }
        }

        /*
        void OnHit(Player target)
        {
            if(target != null && target != shooter )
            {
                if (this.shooter != null)
                {
                    
               
                }

                //target.health -= damage;

            }

        }
        */

        public void Explode()
        {

            Destroy(this.gameObject);

        }
    }
}

