using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A* tile algorithm
/// </summary>
public class AStar : MonoBehaviour
{

    Grid grid;
    public Transform seeker, target;
    public int heuristicSelection; // {"djikstra" , "A*"}
    private AIController aiController;
    void Awake()
    {
        grid = GameObject.Find("Floor").GetComponent<Grid>();
        aiController = GetComponent<AIController>();
    }

    /// <summary>
    /// Find shortest path between start position and target position
    /// </summary>
    /// <param name="startPos"></param>
    /// <param name="targetPos"></param>
    public void FindPath(Vector3 startPos, Vector3 targetPos)
    {

        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        List<Node> closedSet = new List<Node>();


        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];
            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.isWalkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    if (heuristicSelection > 0)
                    {
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                    }
                    neighbour.parent = currentNode;

                    if (!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Saves the shortest path taken, and drawing is handled by grid.cs
    /// </summary>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    void RetracePath(Node source, Node destination)
    {
        List<Node> path = new List<Node>();
        Node currentNode = destination;

        while (currentNode != source)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }

        path.Reverse();

        aiController.path = path;
    }

    // Euclidean Distance
    int GetDistance(Node source, Node destination)
    {
        int distanceX = Mathf.Abs(source.gridX - destination.gridX);
        int distanceY = Mathf.Abs(source.gridY - destination.gridY);

        if (distanceX > distanceY)
        {
            return 14 * distanceY + 10 * (distanceX - distanceY);
        }
        else
        {
            return 14 * distanceX + 10 * (distanceY - distanceX);
        }
    }

}
