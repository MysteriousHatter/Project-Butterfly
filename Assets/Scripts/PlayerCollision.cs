using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] Rigidbody rigid;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
        {
            rigid = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //assign other object tag to variable in order to create switch statement
        tag = other.gameObject.tag;
        Debug.Log("Trigger detected");
        switch (tag)
        {
            case("Collectible"):
                Debug.Log("Hit Collectible");
                Destroy(other.gameObject);
                break;
            case ("Shrine"):
                Debug.Log("Hit Shrine");
                break;
            default:
                Debug.Log("Error, hit object with no tag");
                break;
        }
    }
}
