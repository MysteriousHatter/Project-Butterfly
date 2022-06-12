using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vortex : MonoBehaviour
{
    [SerializeField] float timeToDestory = 5.0f;
    [SerializeField] float speed = 6f;
    [SerializeField] float magnetDistance = 5f;
    private BoxCollider center;
    private int layerMask = 3;

    // Start is called before the first frame update
    void Start()
    {
        center = GetComponentInChildren<BoxCollider>();
        Destroy(this.gameObject, timeToDestory);   
    }

    private void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, magnetDistance, 1 << layerMask);
        for (int i = 0; i < hitColliders.Length; i++)
        {

            if (hitColliders[i].tag == "Collectible" || hitColliders[i].tag == "Enemy")
            {
                //Debug.Log("Object were hitting " + hitColliders[i].gameObject.name);
                Transform collectiable = hitColliders[i].transform;
                Debug.Log("We are hitting " + collectiable.gameObject.name);
                collectiable.position = Vector3.MoveTowards(collectiable.position, center.transform.position, speed * Time.deltaTime);
            }
        }
    }
}
