using UnityEngine;

public class AIController : MonoBehaviour
{
    public enum role { Wander, Chase, Idle, Arrive }
    [HideInInspector]
    public IEnemyState currentState;
    public role aiRole;

    [HideInInspector]
    public WanderState wanderState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public IdleState idleState;
    [HideInInspector]
    public ArriveState arriveState;

    void Awake()
    {
        wanderState = new WanderState(this);
        chaseState = new ChaseState(this);
        idleState = new IdleState(this);
        arriveState = new ArriveState(this);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SelectRole();
        currentState.UpdateState();
    }
    void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
    void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }
    void OnCollisionEnter(Collision other)
    {
        currentState.OnCollisionEnter(other);
    }
    void SelectRole()
    {
        if (aiRole == role.Wander)
        {
            currentState = wanderState;
        }
        else if (aiRole == role.Chase)
        {
            currentState = chaseState;
        }
        else if (aiRole == role.Idle)
        {
            currentState = idleState;
        }
        else if (aiRole == role.Arrive)
        {
            currentState = arriveState;
        }
        
}
