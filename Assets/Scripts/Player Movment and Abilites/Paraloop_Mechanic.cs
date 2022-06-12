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
                    currentNode = new Node(this.GetComponentInParent<Movement>().transform.position);
                    neighbors.Add(currentNode);
                    Debug.Log("Start Prinitng 1");
                    //Debug.Log("Current position " + neighbors[currentIndex].coordinates + "The total count: " + neighbors.Count);
                    currentIndex++;
                    timeToAddPoint = timeToAddPointPlaceholder;
                }
                else
                {
                    startCoordinates = this.transform.position;
                    startNode = new Node(startCoordinates);
                    Debug.Log("Start Prinitng 2");
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
                            Debug.Log("Point for a1 " + neighbors[i].coordinates);
                            Debug.Log("Point for a2 " + neighbors[i + 1].coordinates);
                            Debug.Log("Point for b1 " + neighbors[j].coordinates);
                            Debug.Log("Point for b2 " + neighbors[j + 1].coordinates);

                            Debug.Log("We found a point that intersected");
                            Debug.Log("We found the intersection at " + intersection);

                           // yield return new WaitForSeconds(0.1f);
                            List<Node> connectedCoord = ConnectTheNodes(new Node(intersection));
                            Node intersectionNode = new Node(intersection);
                            connectedCoord[j].connectedTo = intersectionNode;
                            Debug.Log("The final index is " + connectedCoord[j].coordinates + " is connected to " + connectedCoord[j].connectedTo.coordinates);

                            float sqrArea = FindVortexArea(connectedCoord, intersectionNode);
                            Debug.Log("the sqrArea " + sqrArea);
                            if(sqrArea >= 0.1f && sqrArea <= 200f)
                            {
                                Instantiate(vortexPrefab, FindVortexCenter(connectedCoord, intersectionNode), Quaternion.identity);
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
        Debug.Log("The surface area " + area);
        return Math.Abs(area);
        
    }

    private List<Node> ConnectTheNodes(Node IntersectionNode)
    {
        List<Node> placeholder = neighbors;

        int count = 0;
        Debug.Log("Before " + currentIndex);
        for(int i = 0; i + 1 < placeholder.Count; i++)
        {
            count++;
            Debug.Log("The count "  + i + "The total count " + placeholder.Count);
            //TODO: Check if connected nodes are similar to each other
            placeholder[i].connectedTo = placeholder[i + 1];
            Debug.Log("The current node " + neighbors[i].coordinates + " is connected to " + neighbors[i].connectedTo.coordinates);
            Debug.Log("After " + currentIndex);

        }
        //Debug.Log("The final index " + count + "is " + placeholder[count-1].coordinates);
        //placeholder[count - 1].connectedTo = IntersectionNode;

        return placeholder;
    }

    private Vector3 FindVortexCenter(List<Node> connectedNodes, Node inersectionNode)
    {
        Node currentNode = connectedNodes[0].connectedTo;
        Vector3 totalToCenter = Vector3.zero;

        int count = 0;
        while (currentNode.connectedTo != null) 
        {
            count++;
            Debug.Log("The list of current nodes to sum " + currentNode.coordinates + "connected " + currentNode.connectedTo.coordinates);
            totalToCenter += currentNode.coordinates;
            currentNode = currentNode.connectedTo;
        }
        totalToCenter += inersectionNode.coordinates;
        Debug.Log("Before Division " + totalToCenter);
        return totalToCenter /= count;
        Debug.Log("The total " + totalToCenter);


        
    }

    private static bool IsOutOfBounds(int firstPoint, int secondPoint, int listCount)
    {

        Debug.Log("the first point " + firstPoint);
        Debug.Log("The second point " + secondPoint);
        Debug.Log("The list count " + listCount);
        if (firstPoint >= listCount || secondPoint >= listCount)
        {
            Debug.Log("tRUE");
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



        Debug.Log("Line Point 1 " + linePoint1 + "and Line Point 2" + linePoint2);
        Vector3 lineVec3 = linePoint2 - linePoint1;
        Debug.Log("lineVec3 " + lineVec3);
        Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
        Debug.Log("crossVec1and2 " + crossVec1and2 + "is made out of lineVec1 " + lineVec1 + "and " + lineVec2);
        Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);
        Debug.Log("crossVec3and2 " + crossVec3and2 + "is made out of lineVec1 " + lineVec3 + "and " + lineVec2);

        float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);
        Debug.Log("PlaneFactor " + planarFactor);

        //is coplanar, and not parallel
        if (Mathf.Abs(planarFactor) < 0.5f
                && crossVec1and2.sqrMagnitude > 0.5f)
        {
            Debug.Log("An Intersection");
            float s = Vector3.Dot(crossVec3and2, crossVec1and2)
                    / crossVec1and2.sqrMagnitude;
            intersection = linePoint1 + (lineVec1 * s);
            return Tuple.Create(true, intersection);
        }
        else
        {
            Debug.Log("No Intersection");
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
