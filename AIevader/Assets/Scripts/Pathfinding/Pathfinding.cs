using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Pathfinding : MonoBehaviour {

    [Header("Remeber to set the tab to pathfinder and add a rigidbody!")]
    NodeBinaryHeap openHeap;
    PathfindingNode startNode;
    PathfindingNode bestSoFar;
    bool heapOverflow = false;
    List<PathfindingNode> path2Follow;
    PathfindingNode goalNode;
    GameObject makerRef;
    private int maxNumNodes;

    private bool abort = false;
    
    void Start()
    {
        path2Follow = new List<PathfindingNode>();
        makerRef = GameObject.Find("NodesContainer");
        Instantiator tempRef = makerRef.GetComponent<Instantiator>();
        maxNumNodes = tempRef.gridSize * tempRef.gridSize;
        openHeap = new NodeBinaryHeap(2048);
    }

    private void Update()
    {
        if (Input.GetButtonDown("optionPov"))
        {
            abort = true;
        }
    }
    GameObject findClosestNode()
    {
       
        GameObject returnNode = null;
        float closest = Mathf.Infinity;
        float candidateDistance;
       for (int i = 0; i < makerRef.transform.childCount; ++i)
        {
            
            Transform candidateNode = makerRef.transform.GetChild(i);
            candidateDistance = (candidateNode.position - transform.position).sqrMagnitude;
            if (candidateDistance < closest)
            {
                closest = candidateDistance;
                returnNode = candidateNode.gameObject;
            }
        }
       // Debug.Log("Hello Friends");
        return returnNode;
    }

    void CalculateHeuristic(PathfindingNode target, PathfindingNode goal)
    {
        float hValue;
        switch (OptionsController.heuristicNo)
        {
            //Euclidean distance
            case 0:
                hValue = (goal.GetComponent<Transform>().position - 
                    target.GetComponent<Transform>().position).magnitude;
                hValue = GetDistanceInTileValue(hValue, target.transform.GetComponent<Renderer>().bounds.size.x);
                break;
            //Dijkstra
            case 1:
                hValue = 0;
                break;
            //Cluster
            case 2:
                //if not pov do a*
                if (!OptionsController.povMode)
                {
                    hValue = (goal.GetComponent<Transform>().position -
                                        target.GetComponent<Transform>().position).magnitude;
                    hValue = GetDistanceInTileValue(hValue, target.transform.GetComponent<Renderer>().bounds.size.x);
                }
                else if (goal.cluster && target.cluster)
                {
                    hValue = (goal.cluster.GetComponent<Transform>().position -
                        target.cluster.GetComponent<Transform>().position).magnitude;
                    hValue = GetDistanceInTileValue(hValue, target.transform.GetComponent<Renderer>().bounds.size.x);
                }
                else
                {
                    hValue = 0;
                }
                break;
            default:
                hValue = 0;
                break;
        }
        target.Heuristic = hValue;
    }

    float GetDistanceInTileValue(float value, float width)
    {
        return value / width;
    }

    public List<PathfindingNode> FindPath(PathfindingNode goal, int NPCID)
    {

        CleanUp(NPCID);

        startNode = findClosestNode().GetComponent<PathfindingNode>();
        bestSoFar = startNode;
        goalNode = goal;
        
        //We're defining the Heuristic to be 0 on the goal node, regardless of heuristic used.
        goalNode.Heuristic = 0;
        PathfindingNode focus;
        //PathfindingNode focus = startNode.GetComponent<PathfindingNode>();
        openHeap.SortedAdd(startNode.GetComponent<PathfindingNode>());
        //Loop continues until either the openList is empty or the goal node is reached

        //we need a check to see if the focus == the goal

        int debugCtr = 0;
        while (!abort && debugCtr < 2000 && openHeap.currentItemCount != 0)
        {
            focus = openHeap.Pop();
            //Debug.Log(focus);
            if (focus != null)
            {
                for (int i = 0; i < focus.neighbors.Count; i++)
                {
                    PathfindingNode neighbor = focus.neighbors[i];

                    if (neighbor != null)
                    {

                        if (neighbor.heuristicNotComputed()) CalculateHeuristic(neighbor, goalNode); 

                        
                        //if the neighbor is not explored OR if we have a better path
                        if ((!neighbor.explored[NPCID]) || ((neighbor.Heuristic + focus.DistanceSoFar) < neighbor.TotalCost))
                        {
                            if ((neighbor.TotalCost < bestSoFar.TotalCost) || (bestSoFar.Heuristic == -1)) bestSoFar = neighbor;
                            neighbor.DistanceSoFar = focus.DistanceSoFar + 1;
                            neighbor.backReference[NPCID] = focus;
                            if ((!heapOverflow) && (openHeap.currentItemCount < openHeap.items.Length)) { openHeap.SortedAdd(neighbor); }
                            else
                            {
                                heapOverflow = true;
                                return new List<PathfindingNode>();
                            }

                        }
                    }

                }
               // Debug.Log(openHeap.currentItemCount);
                focus.explored[NPCID] = true;
                if ((focus.nodeID == goalNode.nodeID))
                {
                    List<PathfindingNode> finalPath = new List<PathfindingNode>();
                    //caomparing focus.nodeID instead of focus.backreference.nodeID allows the second node found to be inserted in the path
                    while (focus.nodeID != startNode.nodeID)
                    {
                        finalPath.Insert(0, focus);
                        //finalPath.Add(focus.BackReference);
                        focus = focus.backReference[NPCID];
                    }
                    path2Follow = finalPath;
                    DrawPathfinding(NPCID);
                   
                    return finalPath;
                }
            }

            debugCtr++;


        }
        //if we've reached this point, then the openlist search space became too large at some point
        //And our new aim is to return the path leading to the node with the most potential to lead to a path in the future.

        List<PathfindingNode> finalPath2 = new List<PathfindingNode>();
        //Need to know if we reached the goal or not.

        //Now we follow the backreferences we've been setting whenever we updated a node
        //until we reach the initial node, this gives us a path.
    
        while (!abort && bestSoFar.backReference[NPCID].nodeID != startNode.nodeID)
        {
            finalPath2.Insert(0, bestSoFar);
            bestSoFar = bestSoFar.backReference[NPCID];
        }
        path2Follow = finalPath2;
        DrawPathfinding(NPCID);
            
        abort = false;
        return finalPath2;
    }

    public void SetGoal(PathfindingNode incomingNode)
    {
        goalNode = incomingNode;
    }

    void DrawPathfinding(int NPCID)
    {
        //start purple
        //end green
        //open blue
        //closed red
        //path itself orange
        //uninvolved black

        foreach (Transform node in makerRef.transform)
        {
            if (node.GetComponent<PathfindingNode>().explored[NPCID] == true)
            {
                //node.GetComponent<Renderer>().material.color = new Color(1.0f,0.0f,0.0f,1.0f);
            }
        }
        

       

        foreach (PathfindingNode node3 in openHeap.items)
        {
            if (node3 != null)
            {
                node3.gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
            }
        }
        foreach (PathfindingNode node2 in path2Follow)
        {
            node2.gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        }
        startNode.gameObject.GetComponent<Renderer>().material.color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
        goalNode.gameObject.GetComponent<Renderer>().material.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    }

    void CleanUp(int NPCID)
    {
        
        foreach (Transform node in makerRef.transform)
        {
            PathfindingNode cache = node.GetComponent<PathfindingNode>();
            cache.explored[NPCID] = false;
            cache.Heuristic = -1;
            if (cache.backReference.Count >= NPCID)
            {
                cache.backReference[NPCID] = null;
            }
            node.gameObject.GetComponent<Renderer>().material.color = new Color(Random.Range(0.8f, 1.0f),
                    Random.Range(0.8f, 1.0f),
                    Random.Range(0.8f, 1.0f), 1);
        }
        openHeap = new NodeBinaryHeap(2048);
        openHeap.currentItemCount = 0;
        
        path2Follow.Clear();

    }
    
}
