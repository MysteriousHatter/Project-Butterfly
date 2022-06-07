using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    public GameObject collider;
    public GameObject player;
    [SerializeField] float timePenalty;

    void Start()
    {
        collider = GameObject.FindWithTag("Player");
        player = collider.transform.parent.gameObject;
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        // the following two lines calculate if the other object is in front or behind the player
        Vector3 heading = transform.position - other.transform.position;
        float dot = Vector3.Dot(heading, other.transform.forward);
        // ensures that the other object is a hazard
        var playerTag = other.gameObject.tag;
        if (playerTag == "Player")
        { 
            Debug.Log("Hit Hazard");
            player.GetComponent<Movement>().HurtPlayer(heading, dot);
            FindObjectOfType<GameplayUIBehavior>().setTime(timePenalty);
        }
    }
}
