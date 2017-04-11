using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Draws the shortest path using A* tile algorithm
/// </summary>
public class Grid : MonoBehaviour
{

    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public Node[,] grid;

    float nodeDiameter;
    int gridSizeX, gridSizeY;

    public List<Node> path;

    // Use this for initialization
    void Start()
    {
        nodeDiameter = nodeRadius * 2f;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    /// <summary>
    /// Get neighbouring nodes from supplied node
    /// </summary>
    /// <param name="node"></param>
    /// <returns>Neighbours of supplied node</returns>
    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                // Current Node
                if (x == 0 && y == 0)
                {
                    continue;
                }

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }


    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Convert world position into its corresponding node from the grid
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <returns>The corresponding node</returns>
    public Node NodeFromWorldPoint(Vector3 worldPosition)
    {
        float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
        float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;

        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return grid[x, y];
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));

        if (grid != null)
        {
            foreach (Node n in grid)
            {
                Gizmos.color = (n.isWalkable) ? Color.green : Color.red;
                if (path != null)
                {
                    if (path.Contains(n))
                    {
                        Gizmos.color = Color.blue;
                    }
                }
                Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter - 0.04f));
            }
        }
    }
}
