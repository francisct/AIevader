using System;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public enum role { Wander, Chase, Idle, Arrive, Combat }
    [HideInInspector]
    public IEnemyState currentState;
    public role aiRole;
    public AIManager aiManager;
    public float runningAnimationTrigger = 4f;
    public int hitPoints = 50;
    public AudioClip spawnSound;
    public AudioClip chaseSound;
    public AudioClip deathSound;
    public AudioClip combatSound1;
    public AudioClip combatSound2;
    public AudioClip combatSound3;

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
    [HideInInspector]
    public Vector3 velocity;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public AudioSource audioSource;
    private bool playedSpawnSound = false;
    void Awake()
    {
        wanderState = new WanderState(this);
        chaseState = new ChaseState(this);
        idleState = new IdleState(this);
        arriveState = new ArriveState(this);
        combatState = new CombatState(this);
        currentState = wanderState;
        steeringAlign = GetComponent<SteeringAlign>();
        steeringArrive = GetComponent<SteeringArrive>();
        steeringSeek = GetComponent<SteeringSeek>();
        steeringWander = GetComponent<SteeringWander>();
        audioSource = GetComponent<AudioSource>();
        velocity = Vector3.zero;
    }
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectRole();
        if (!animator.GetCurrentAnimatorStateInfo(0).IsTag("Spawn"))
        {
            currentState.UpdateState();
        }
        else if (!playedSpawnSound)
        {
            audioSource.PlayOneShot(spawnSound);
        }
        UpdateAnimation();
        if (hitPoints <= 0)
        {
            audioSource.PlayOneShot(deathSound);
            AIManager.AIKilled(this);
        }
    }

    private void UpdateAnimation()
    {
        var speed = velocity.magnitude;
        if (speed > runningAnimationTrigger)
        {
            animator.SetBool("isRunning", true);
        }
        else if (speed > 0f)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);

        }
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
