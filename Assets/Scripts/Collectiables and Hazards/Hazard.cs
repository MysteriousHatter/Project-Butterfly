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
        // ensures that the other object is a hazard
        var playerTag = other.gameObject.tag;
        if (playerTag == "Player")
        { 
            Debug.Log("Hit Hazard");
            AkSoundEngine.PostEvent("PlayerHurt", this.gameObject);
            Vector3 pushDirection =  other.transform.position - transform.position;
            player.GetComponent<Movement>().MoveBack(pushDirection.normalized);
            FindObjectOfType<GameplayUIBehavior>().setTime(timePenalty);
        }
    }
}
