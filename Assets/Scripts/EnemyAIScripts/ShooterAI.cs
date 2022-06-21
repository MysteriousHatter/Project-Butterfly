using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class ShooterAI : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;
    private Animator animator;
    [SerializeField] private float distanceCheck = 5f;

    Vector3 directionToShoot;
    // Start is called before the first frame update
    void Start()
    {
            animator = GetComponentInChildren<Animator>();
            player = FindObjectOfType<Paraloop_Mechanic>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if(animator == null) { Destroy(this.gameObject); }
    }
    [Task]
    bool IsPlayerClose()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < distanceCheck)
        {
            Debug.Log("player is close " + Vector3.Distance(player.transform.position, transform.position));
            animator.SetBool("Jump", true);
            animator.SetBool("Idle", false);
            animator.SetBool("Attack", false);
            return true;
        }
        else
        {
            print("player isnt close" + Vector3.Distance(player.transform.position, transform.position));
            animator.SetBool("Jump", false);
            animator.SetBool("Attack", false);
            animator.SetBool("Idle", true);
            return false;
           
        }
    }
    [Task]
    void AimAt_Player(string direction)
    {
        Debug.Log("input direction" + direction);
        directionToShoot = player.transform.position - transform.position;
        Task.current.Succeed();
    }

    [Task]
    void Fire() {
        animator.SetBool("Jump", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Attack",true);
        GameObject bullet = GameObject.Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.transform.up = -Vector3.forward;
        Task.current.Succeed();
    }

    [Task]
    bool SetTarget_Angle(float angle)
    {
        var p = this.transform.position + Quaternion.AngleAxis(angle, Vector3.up) * this.transform.forward;
        //self.SetTarget(p);
        return true;
    }
}
