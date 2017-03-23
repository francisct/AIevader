using System;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private Transform chaseTarget;
    private AIController aiController;
    public ChaseState(AIController aiController)
    {
        chaseTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        this.aiController = aiController;
    }
    public void OnCollisionEnter(Collision other)
    {
        ;
    }

    public void OnTriggerEnter(Collider other)
    {
        ;//maybe an attack range and go to attack state
    }

    public void OnTriggerExit(Collider other)
    {
        ;
    }

    public void ToArriveState()
    {
        aiController.aiRole = AIController.role.Arrive;
    }

    public void ToChaseState()
    {
        ;
    }

    public void ToIdleState()
    {
        aiController.aiRole = AIController.role.Idle;
    }

    public void ToWanderState()
    {
        aiController.aiRole = AIController.role.Wander;
    }

    public void UpdateState()
    {
        //Set movement target or pathfinding target to chaseTarget.position
    }
}
