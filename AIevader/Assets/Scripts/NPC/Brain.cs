using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour {

    NPC pathfinder;
    [SerializeField]
    TargetController player;

	// Use this for initialization
	void Start () {
        pathfinder = GetComponent<NPC>();
	}
	
	// Update is called once per frame
	void Update () {
        if (INeedNewPath())
        {
            pathfinder.SetGoalNode(player.GetCurrenTile());
            //SelectNewGoal();
        }


    }

    bool INeedNewPath()
    {
        return pathfinder.arrivedToGoal;
    }

    void SelectNewGoal()
    {
        PathfindingNode[] nodes = GameObject.Find("Scripts").GetComponentsInChildren<PathfindingNode>();
        int rnd = Random.Range(0, nodes.Length);
        pathfinder.SetGoalNode(nodes[rnd]);
    }
    
}
