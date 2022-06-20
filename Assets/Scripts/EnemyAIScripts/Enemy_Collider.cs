using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Collider : MonoBehaviour
{
    public GameObject playerCollider;
    public GameObject player;
    [SerializeField] float timePenalty;
    [SerializeField] GameObject FX;

    [SerializeField] Transform parent;

    void Start()
    {
        playerCollider = GameObject.FindWithTag("Player");
        player = playerCollider.transform.parent.gameObject;
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
            AudioManager.instance.HazardSFX();
            Vector3 pushDirection = other.transform.position - transform.position;
            player.GetComponent<Movement>().MoveBack(pushDirection.normalized);
            FindObjectOfType<GameplayUIBehavior>().setTime(timePenalty);
        }
        else if(playerTag == "Drill")
        {
            Debug.Log("Destroy enemy");
            GameObject fx = Instantiate(FX, transform.position, Quaternion.identity);
            fx.transform.parent = parent;
            Destroy(this.gameObject);
        }
        else if(playerTag == "Vortex")
        {
            GameObject fx = Instantiate(FX, transform.position, Quaternion.identity);
            fx.transform.parent = parent;
            Destroy(this.gameObject);
        }
    }
}
