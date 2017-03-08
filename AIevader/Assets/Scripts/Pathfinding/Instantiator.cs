using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Instantiator : MonoBehaviour {

    public int gridSize;
    int centerOffset;
    public GameObject markers;
    public GameObject clickedNode;
    public GameObject pathfinder;
    public static bool ready;

    [SerializeField]
    GameObject povNodeTemplatesParent;
    // Use this for initialization
    void Start() {
        generateNodes();
        makePlayer();
    }

    // Update is called once per frame
    void Update() {

    }

    void makePlayer()
    {
        //GameObject theSubject = (GameObject)Instantiate(pathfinder, new Vector3(0.0f, 0.5f, 5.0f), Quaternion.identity);
        ready = true;
    }
    /*Instantiates a bunch of cubes and sets its neighbors.  Pathfinding can be done on
    * a Hierarchical Grid structure or an Undirected Graph.  This function just uses an
    *  array as a cheap way of keeping the GameObject references in order, and discards
    *   the array structure afterward.At the end, what we have is a bunch of GameObjects
    *   with a PathfindingNode script attached, which has a list of its neighbors.  
    * This means that any GameObject/Script that wants to pathfind can climb through the
    * rest of the nodes via this reference without needing access to some kind of meta-structure
    * that stores all the nodes.
    */
    public void generateNodes()
    {
        clearNodes();

        if (OptionsController.povMode) generatePovNodes();
        else generateRegularNodes();
    }

    void clearNodes()
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    void generatePovNodes()
    {
        GameObject povNodeTemplatesParentCopy = Object.Instantiate(povNodeTemplatesParent);
        PathfindingNode[] povNodeCopies = povNodeTemplatesParentCopy.GetComponentsInChildren<PathfindingNode>();
        for (int i = 0; i < povNodeCopies.Length; i++)
        {
            povNodeCopies[i].transform.SetParent(transform);
            povNodeCopies[i].GetComponent<PathfindingNode>().nodeID = i;
        }
    }

    void generateRegularNodes()
    {
        GameObject[,] tempGrid = new GameObject[gridSize, gridSize];

        float cubeScale = 100.0f / gridSize;
        for (int i = 0; i < gridSize; ++i)
        {
            for (int j = 0; j < gridSize; ++j)
            {
                GameObject marker = (GameObject)Instantiate(markers,
                    new Vector3(-50f + (0.5f * cubeScale) + (cubeScale * i),
                        0f,
                        50f - (0.5f * cubeScale) - cubeScale * j),
                    Quaternion.identity);
                marker.GetComponent<PathfindingNode>().nodeID = (i * gridSize) + j;
                marker.transform.localScale = new Vector3(100.0f / gridSize, 1f, 100.0f / gridSize);
                marker.GetComponent<Renderer>().material.color =
                    new Color(Random.Range(0.8f, 1.0f),
                    Random.Range(0.8f, 1.0f),
                    Random.Range(0.8f, 1.0f), 1);
                marker.transform.SetParent(transform);
                tempGrid[i, j] = marker;
                marker.GetComponent<PathfindingNode>().wallOnTop();
            }
        }

        for (int i = 0; i < gridSize; ++i)
        {
            for (int j = 0; j < gridSize; ++j)
            {
                if (tempGrid[i, j] != null)
                {
                    PathfindingNode reference = tempGrid[i, j].GetComponent<PathfindingNode>();
                    if ((i + 1) < gridSize)
                    {
                        if (tempGrid[i + 1, j] != null)
                            reference.neighbors.Add(tempGrid[i + 1, j].GetComponent<PathfindingNode>());

                    }
                    if ((j + 1) < gridSize)
                    {
                        if (tempGrid[i, j + 1] != null)
                            reference.neighbors.Add(tempGrid[i, j + 1].GetComponent<PathfindingNode>());

                    }
                    if ((i - 1) >= 0)
                    {
                        if (tempGrid[i - 1, j] != null)
                            reference.neighbors.Add(tempGrid[i - 1, j].GetComponent<PathfindingNode>());
                    }
                    if ((j - 1) >= 0)
                    {
                        if (tempGrid[i, j - 1] != null)
                            reference.neighbors.Add(tempGrid[i, j - 1].GetComponent<PathfindingNode>());
                    }
                    
                }
            }
        }
    }

}
