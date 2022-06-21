using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnNotStatue : MonoBehaviour
{
    SpawnManager spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn = FindObjectOfType<SpawnManager>();
    }

    private void OnTriggerExit(Collider other)
    {
        spawn.HandleNewLap(false);
    }
}
