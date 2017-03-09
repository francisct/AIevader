using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour {

    private float seekSpeed = 20.0f;
    private float relativeSpeed;
    public Transform currentTarget;
    public List<PathfindingNode> targetList = new List<PathfindingNode>();
    public PathfindingNode goalNode;
    public bool arrivedToGoal = false;
    S_Arrive s_arrive;
    public int NPCID;
    
    private float newGoalCtr;
    private float timeBeforeNewGoal = 2f;
    void Start()
    {
        NPCID = GameObject.FindGameObjectsWithTag("Pathfinder").Length-1;
        /*foreach(GameObject node in GameObject.FindGameObjectsWithTag("Node"))
        {
            node.GetComponent<PathfindingNode>().explored.Add(false);
        }*/
        if (!OptionsController.povMode) relativeSpeed = (GameObject.Find("NodesContainer").GetComponent<Instantiator>().gridSize) / 5.0f;
        else relativeSpeed = 2f;

        s_arrive = GetComponent<S_Arrive>();
    }

    void Update()
    {
        if (Input.GetButtonDown("optionPov"))
        {
            Abort();
        }
        if (Instantiator.ready)
            FollowPath();

        if (newGoalCtr < 2f)
            newGoalCtr += Time.deltaTime;
        else
        {
            newGoalCtr = 0.0f;
            ResetGoal();
        }
    }

    void Abort()
    {
        targetList = new List<PathfindingNode>();
        goalNode = null;
        currentTarget = null;
    }


    void Seek()
    {
            Vector3 direction = (currentTarget.transform.position - transform.position).normalized;
            float realSpeed = seekSpeed * relativeSpeed;
            GetComponent<Rigidbody>().AddForce((direction * realSpeed));
    }

    void SetNewTarget()
    {
        if (targetList.Count != 0)
        {
            currentTarget = targetList[0].GetComponentInParent<Transform>();
            targetList.RemoveAt(0);
        }
        else
        {
            arrivedToGoal = true;
        }
    }

    void FollowPath()
    {
        if (goalNode != null)
        {
            if (targetList.Count == 0)
            {
                targetList = GetComponentInParent<Pathfinding>().FindPath(goalNode, NPCID);
                goalNode = null;
            }

        }
        if ((currentTarget == null) || ((new Vector3(transform.position.x, currentTarget.position.y, transform.position.z) - currentTarget.position).sqrMagnitude < 1.0f))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f,0.0f,0.0f);
            SetNewTarget();
        }
        if (currentTarget != null)
        {
            s_arrive.Move(currentTarget.position, seekSpeed * relativeSpeed);
        }
    }

    public void SetGoalNode(PathfindingNode goalFromClickedNode)
    {
        goalNode = goalFromClickedNode;
        arrivedToGoal = false;
    }

    private void ResetGoal()
    {
        targetList = new List<PathfindingNode>();
        arrivedToGoal = true;
    }
}
