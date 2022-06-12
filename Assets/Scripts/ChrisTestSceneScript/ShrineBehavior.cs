using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
public class ShrineBehavior : MonoBehaviour
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
        if (other.GetComponentInParent<Movement>() != null)
        {
            Debug.Log("Have we freed a statue " + GameplayManager.Instance.getStatueIsFree());
            GameplayManager.Instance.OnLoopCompleted();

        }
    }
}
