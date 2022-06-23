using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Statue Behavior Config", fileName = "StatueBehaviorConfig" )]
public class StatueBehaviorConfig : ScriptableObject
{
    public GameObject SpawnLocation;
    public int health; 

}
