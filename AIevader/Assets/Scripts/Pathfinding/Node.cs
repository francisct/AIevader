﻿using UnityEngine;
using System.Collections;

/// <summary>
/// A* node
/// </summary>
public class Node
{

    public bool isWalkable;
    public Vector3 worldPosition;
    public int gridX;
    public int gridY;

    public int gCost;
    public int hCost;
    public Node parent;

    public Node(bool isWalkable, Vector3 worldPosition, int gridX, int gridY)
    {
        this.isWalkable = isWalkable;
        this.worldPosition = worldPosition;
        this.gridX = gridX;
        this.gridY = gridY;
    }

    public Node(bool isWalkable, Vector3 worldPosition)
    {
        this.isWalkable = isWalkable;
        this.worldPosition = worldPosition;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
