using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Terrain")
        {
            
            Debug.Log("TerrainCollision");
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            // push player on y axis
            this.GetComponentInParent<Movement>().deltaY -= collision.contacts[0].separation * Mathf.Sign(collision.contacts[0].normal.y) * 2f;
            Debug.Log("TerrainCollision");
        }
    }
}
