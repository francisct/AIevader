using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour {

    [SerializeField]
    private float seekSpeed = 20.0f;
    private float relativeSpeed;
    [System.NonSerialized]
    public Transform currentTarget;
    [System.NonSerialized]
    public List<PathfindingNode> targetList = new List<PathfindingNode>();
    [System.NonSerialized]
    public PathfindingNode goalNode;
    [System.NonSerialized]
    public bool arrivedToGoal = false;
    Movement movement;
    [System.NonSerialized]
    public int NPCID;
    
    private float newGoalCtr;
    private float timeBeforeNewGoal = 2f;
    private bool dead;
    Animator anim;
    BoxCollider boxCollider;
    Rigidbody rigidBody;


    void Start()
    {
        NPCID = GameObject.FindGameObjectsWithTag("Pathfinder").Length-1;
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        rigidBody = GetComponent<Rigidbody>();
        if (!OptionsController.povMode) relativeSpeed = (GameObject.Find("NodesContainer").GetComponent<Instantiator>().gridSize) / 5.0f;
        else relativeSpeed = 2f;

        movement = GetComponent<Movement>();
    }

    void Update()
    {
        if (dead)
            Die();
        if (Input.GetButtonDown("optionPov"))
        {
            anim.SetTrigger("dead");
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
                anim.SetBool("moving", false);
            }

        }
        if ((currentTarget == null) || ((new Vector3(transform.position.x, currentTarget.position.y, transform.position.z) - currentTarget.position).sqrMagnitude < 1.0f))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f,0.0f,0.0f);
            SetNewTarget();
        }
        if (currentTarget != null)
        {
            anim.SetBool("moving", true);
            movement.Seek(new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z), seekSpeed * relativeSpeed);
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

    private void Die()
    {
        Abort();
        anim.SetBool("dead", true);
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
        boxCollider.center = new Vector3(0, 1.7f, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.tag == "Spikes")
        {
            dead = true;
            Debug.Log("should be dying" + dead);
        }
            
    }

}
