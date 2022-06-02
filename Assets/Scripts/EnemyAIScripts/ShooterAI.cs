using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class ShooterAI : MonoBehaviour
{
    public GameObject player;
    public GameObject bulletPrefab;

    Vector3 directionToShoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [Task]
    bool IsPlayerClose()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < 10f)
        {
            Debug.Log("player is close");
            return true;
        }
        else
        {
            print("player isnt close" + Vector3.Distance(player.transform.position, transform.position));
            return false;
        }
    }
    [Task]
    void AimAt_Player()
    {
        directionToShoot = player.transform.position - transform.position;
        Task.current.Succeed();
    }

    [Task]
    void Fire() {
        GameObject bullet = GameObject.Instantiate(bulletPrefab);
        bullet.transform.position = transform.position;
        bullet.transform.forward = directionToShoot;
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
