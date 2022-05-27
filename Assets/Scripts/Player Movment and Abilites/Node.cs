using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Node 
{
    public Vector3 coordinates;
    public bool isPath;
    public Node connectedTo;

    public Node(Vector3 coordinates)
    {
        this.coordinates = coordinates;
    }

    
}
