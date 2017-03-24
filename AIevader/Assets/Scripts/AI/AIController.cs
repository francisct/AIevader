using UnityEngine;

public class AIController : MonoBehaviour
{
    public enum role { Wander, Chase, Idle, Arrive, Combat }
    [HideInInspector]
    public IEnemyState currentState;
    public role aiRole;

    public int hitPoints = 50;

    [HideInInspector]
    public WanderState wanderState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public IdleState idleState;
    [HideInInspector]
    public ArriveState arriveState;
    [HideInInspector]
    public CombatState combatState;
    [HideInInspector]
    public SteeringAlign steeringAlign;
    [HideInInspector]
    public SteeringArrive steeringArrive;
    [HideInInspector]
    public SteeringSeek steeringSeek;
    [HideInInspector]
    public SteeringWander steeringWander;

    void Awake()
    {
        wanderState = new WanderState(this);
        chaseState = new ChaseState(this);
        idleState = new IdleState(this);
        arriveState = new ArriveState(this);
        combatState = new CombatState(this);

        steeringAlign = GetComponent<SteeringAlign>();
        steeringArrive = GetComponent<SteeringArrive>();
        steeringSeek = GetComponent<SteeringSeek>();
        steeringWander = GetComponent<SteeringWander>();
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
        if (currentState != wanderState && aiRole == role.Wander)
        {
            currentState = wanderState;
        }
        else if (currentState != chaseState && aiRole == role.Chase)
        {
            currentState = chaseState;
        }
        else if (currentState != idleState && aiRole == role.Idle)
        {
            currentState = idleState;
        }
        else if (currentState != arriveState && aiRole == role.Arrive)
        {
            currentState = arriveState;
        }
        else if (currentState != combatState && aiRole == role.Combat)
        {
            currentState = combatState;
        }
    }
        
}
