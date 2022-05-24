using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpBehavior : MonoBehaviour
{

    public GameObject[] subParticles;
    public GameObject parent;
    GameObject player;
    bool Collected = false;
    float lerpValue = 0;
    float activeObjects;
    // Start is called before the first frame update
    void Start()
    {
        activeObjects = subParticles.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Collected)
        {
            lerpValue += Time.deltaTime * 0.1f;
            foreach(GameObject subParticle in subParticles)
            {
                subParticle.transform.position = Vector3.Lerp(subParticle.transform.position, player.transform.position, lerpValue);
                if(subParticle.activeSelf && Vector3.Distance(subParticle.transform.position, player.transform.position) < 0.02f)
                {
                    subParticle.SetActive(false);
                    activeObjects--;
                }
            }

        }
        if (activeObjects == 0)
        {
            Destroy(parent);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collected");
        Collected = true;
        player = other.gameObject;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collected");
    }
}
