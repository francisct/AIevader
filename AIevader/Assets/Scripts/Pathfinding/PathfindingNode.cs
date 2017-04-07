using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathfindingNode : MonoBehaviour
{
    
    [System.NonSerialized]
    public int nodeID;
    private float heuristic;
    [System.NonSerialized]
    public List<bool> explored = new List<bool>();
    private float distanceSoFar;
    private int heapIndex;
    [System.NonSerialized]
    public List<PathfindingNode> backReference = new List<PathfindingNode>();
    [System.NonSerialized]
    public List<PathfindingNode> neighbors = new List<PathfindingNode>();

    [System.NonSerialized]
    public PathfindingNode cluster;
    //public List<GameObject> neighbors;
    [SerializeField]
    LayerMask wallLayer;

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.FindGameObjectWithTag("Pathfinder").GetComponent<NPC>().SetGoalNode(this);
        }
    }


    void Start()
    {
        //neighbors = new List<GameObject>();
        heuristic = -1;
        wallOnTop();
    }


    public static bool operator >(PathfindingNode one, PathfindingNode two)
    {
        if (one.heuristic > two.heuristic)
        {
            return true;
        }
        else {
            return false;
        }
    }
    public static bool operator <(PathfindingNode one, PathfindingNode two)
    {
        if (one.heuristic < two.heuristic)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool operator >=(PathfindingNode one, PathfindingNode two)
    {
        if (one.heuristic >= two.heuristic)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool operator <=(PathfindingNode one, PathfindingNode two)
    {
        if (one.heuristic <= two.heuristic)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public int CompareTo(PathfindingNode nodeToCompare)
    {
        //return HeapIndex.CompareTo(nodeToCompare.HeapIndex);
        int compare = TotalCost.CompareTo(nodeToCompare.TotalCost);
        if (compare == 0)
        {
            compare = heuristic.CompareTo(nodeToCompare.heuristic);
        }
        return -compare;
    }

    public int HeapIndex
    {
        get
        {
            return heapIndex;
        }
        set
        {
            heapIndex = value;
        }
    }

    public float TotalCost
    {
        get {
            return (heuristic + distanceSoFar);
        }
    }

    public float Heuristic
    {
        get
        {
            return heuristic;
        }

        set
        {
            heuristic = value;
        }
    }

    

    public float DistanceSoFar
    {
        get
        {
            return distanceSoFar;
        }

        set
        {
            distanceSoFar = value;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Wall")
            Object.Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Wall")
            Object.Destroy(gameObject);
    }

    public void wallOnTop()
    {
        RaycastHit hit;
        float width = GetComponent<BoxCollider>().bounds.size.x;
        if (Physics.SphereCast(transform.position - transform.up * 5, 1.0f, new Vector3(0,1,0), out hit, 14, wallLayer))
        {
            if (hit.transform.tag == "Wall") Object.Destroy(gameObject);
        }
    }

    public bool heuristicNotComputed()
    {
        return Heuristic == -1;
    }

}
