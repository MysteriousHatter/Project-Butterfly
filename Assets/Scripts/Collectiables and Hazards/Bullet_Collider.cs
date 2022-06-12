using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda.Examples.Shooter;

public class Bullet_Collider : MonoBehaviour
{
    public GameObject playerCollider;
    public GameObject player;
    [SerializeField] float timePenalty;
    private Bullet bullet;

    void Start()
    {
        playerCollider = GameObject.FindWithTag("Player");
        player = playerCollider.transform.parent.gameObject;
        bullet = GetComponent<Bullet>();
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
            Vector3 pushDirection = other.transform.position - transform.position;
            player.GetComponent<Movement>().MoveBack(pushDirection.normalized);
            FindObjectOfType<GameplayUIBehavior>().setTime(timePenalty);
            bullet.Explode();
        }
    }
}
