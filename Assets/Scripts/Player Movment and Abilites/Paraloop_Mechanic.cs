using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Paraloop_Mechanic : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField] Vector3 startCoordinates;
    
    [SerializeField] Node startNode;
    [SerializeField] Node intersectionNode;

    [SerializeField] List<Node> neighbors = new List<Node>();
    [SerializeField] int currentIndex = 0;
    [SerializeField] bool startPath;

    [SerializeField] private float timeToAddPoint = 4f;
    float timeToDeletePointPlaceholder;

    [SerializeField] private float timeToDeletePoint = 4f;
    float timeToAddPointPlaceholder;

    [SerializeField] private GameObject vortexPrefab;


    void Start()
    {
        startPath = true;
        timeToAddPointPlaceholder = timeToAddPoint;
        timeToDeletePointPlaceholder = timeToDeletePoint;
        //StartCoroutine(InstantiateTransformations());
        StartCoroutine(GetLineIntersection());
    }

    public void InstantiateTransformations(bool instantiatePoints)
    {
        if (instantiatePoints)
        {
            Node currentNode;

            //yield return new WaitForSeconds(0.5f);
            //targetIndex++;
            timeToAddPoint -= Time.deltaTime;
            if (timeToAddPoint <= 0f)
            {

                if (startNode == null)
                {
                    currentNode = new Node(this.transform.localPosition);
                    neighbors.Add(currentNode);
                    //Debug.Log("Current position " + neighbors[currentIndex].coordinates + "The total count: " + neighbors.Count);
                    currentIndex++;
                    timeToAddPoint = timeToAddPointPlaceholder;
                }
                else
                {
                    startCoordinates = this.transform.localPosition;
                    startNode = new Node(startCoordinates);
                    neighbors.Add(startNode);
                    //Debug.Log("The Start Position " + neighbors[currentIndex]);
                    currentIndex++;
                    timeToAddPoint = timeToAddPointPlaceholder;
                }

            }
        }
        else
        {
            ClearNeighbors();
        }

    }

    IEnumerator GetLineIntersection()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            for (int i = 0; i < neighbors.Count; i++)
            {
                for (int j = i + 2; j < neighbors.Count; j++)
                {
                    Vector3 intersection;
                    Vector3 bDiff;
                    Vector3 aDiff = neighbors[i + 1].coordinates - neighbors[i].coordinates;

                    if (IsOutOfBounds(j, j + 1, neighbors.Count)) { bDiff = aDiff; }
                    else { bDiff = neighbors[j + 1].coordinates - neighbors[j].coordinates; }
                    

                    //out, were basically declaring a varibale in the method
                    if (LineLineIntersection(out intersection, neighbors[i].coordinates, aDiff, neighbors[j].coordinates, bDiff).Item1)
                    {
                        float aSqrMagnitude = aDiff.sqrMagnitude;
                        float bSqrMagnitude = bDiff.sqrMagnitude;

                        if ((intersection - neighbors[i].coordinates).sqrMagnitude <= aSqrMagnitude
                         && (intersection - neighbors[i + 1].coordinates).sqrMagnitude <= aSqrMagnitude
                         && (intersection - neighbors[j].coordinates).sqrMagnitude <= bSqrMagnitude
                         && (intersection - neighbors[j + 1].coordinates).sqrMagnitude <= bSqrMagnitude)
                        {
  

                           // yield return new WaitForSeconds(0.1f);
                            List<Node> connectedCoord = ConnectTheNodes(new Node(intersection),neighbors[i], neighbors[j + 1]);
                            Node intersectionNode = new Node(intersection);
                            connectedCoord[j].connectedTo = intersectionNode;

                            float sqrArea = FindVortexArea(connectedCoord, intersectionNode);
                            if(sqrArea >= 0.1f && sqrArea <= 200f)
                            {
                                GameObject instance = Instantiate(vortexPrefab, Vector3.zero, Quaternion.identity) as GameObject;
                                instance.transform.position = FindVortexCenter(connectedCoord, intersectionNode);
                                neighbors.RemoveRange(0, neighbors.Count - 1);
                            }
                            yield return null;
                           // yield return new WaitForSeconds(10.0f);


                        }
                    }

                }
            }
            yield return new WaitForSeconds(0.2f);
        }

    }

    private float FindVortexArea(List<Node> connectedCoord, Node intersectionNode)
    {
        connectedCoord.Add(intersectionNode);
        int num_points = connectedCoord.Count;

        

        float area = 0;
        for (int i = 0; i < num_points; i++)
        {
            if (i != num_points - 1)
            {
                float mulA = connectedCoord[i].coordinates.x * connectedCoord[i + 1].coordinates.y;
                float mulB = connectedCoord[i + 1].coordinates.x * connectedCoord[i].coordinates.y;
                area = area + (mulA - mulB);
            }
            else
            {
                float mulA = connectedCoord[i].coordinates.x * connectedCoord[0].coordinates.y;
                float mulB = connectedCoord[0].coordinates.x * connectedCoord[i].coordinates.y;
                area = area + (mulA - mulB);
            }

        }

        area /= 2f;
        return Math.Abs(area);
        
    }

    private List<Node> ConnectTheNodes(Node IntersectionNode, Node firstPoint, Node lastPoint)
    {
        List<Node> placeholder = neighbors;

        int count = 0;
        for (int i = 0; placeholder[i].connectedTo != null; i++)
        {
            count++;
            //TODO: Check if connected nodes are similar to each other
            if (placeholder[i].connectedTo != placeholder[i + 1])
            {
                placeholder[i].connectedTo = placeholder[i + 1];
            }
        }

            return placeholder;
    }

    private Vector3 FindVortexCenter(List<Node> connectedNodes, Node inersectionNode)
    {
        Node currentNode = connectedNodes[0].connectedTo;
        Vector3 totalToCenter = Vector3.zero;



        var totalX = 0f;
        var totalY = 0f;
        var totalZ = 0f;

        foreach (var player in connectedNodes)
        {
            totalX += player.coordinates.x;
            totalY += player.coordinates.y;
            totalZ += player.coordinates.z;
        }

        //totalX += inersectionNode.coordinates.x;
        //totalY += inersectionNode.coordinates.y;
        //totalZ += inersectionNode.coordinates.z;

        var centerX = totalX / connectedNodes.Count;
        var centerY = totalY / connectedNodes.Count;
        var centerZ = totalZ / connectedNodes.Count;

        return new Vector3(centerX,centerY,centerZ);
        
    }

    private static bool IsOutOfBounds(int firstPoint, int secondPoint, int listCount)
    {

        if (firstPoint >= listCount || secondPoint >= listCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Tuple<bool, Vector3> LineLineIntersection(out Vector3 intersection, Vector3 linePoint1,
        Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
    {



        Vector3 lineVec3 = linePoint2 - linePoint1;
        Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
        Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);

        float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

        //is coplanar, and not parallel
        if (Mathf.Abs(planarFactor) < 0.5f
                && crossVec1and2.sqrMagnitude > 0.5f)
        {
            float s = Vector3.Dot(crossVec3and2, crossVec1and2)
                    / crossVec1and2.sqrMagnitude;
            intersection = linePoint1 + (lineVec1 * s);
            return Tuple.Create(true, intersection);
        }
        else
        {
            intersection = Vector3.zero;
            return Tuple.Create(false, intersection);
        }
    }



    public void OnDrawGizmos()
    {
        if (neighbors != null)
        {
            if (currentIndex >= 4)
            {
                for (int i = 0; i < neighbors.Count; i++)
                {
                    Gizmos.color = Color.black;
                    Gizmos.DrawCube(neighbors[i].coordinates, Vector3.one);

                    if (i != 0)
                    {
                        //Gizmos.DrawLine(listOfTransfromations[i + 1], transform.position);
                        Gizmos.DrawLine(neighbors[i - 1].coordinates, neighbors[i].coordinates);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    { 
        timeToDeletePoint -= Time.deltaTime;
        if(timeToDeletePoint <= 0f)
        {
            if (neighbors.Count > 10)
            {
                neighbors.RemoveRange(0, 1);
                timeToDeletePoint = timeToDeletePointPlaceholder;
                
            }
        }
    }

    public void ClearNeighbors()
    {
        neighbors.Clear();
    }


}
