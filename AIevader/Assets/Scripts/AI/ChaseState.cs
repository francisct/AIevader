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
        if(other.tag == "Player")
        {
            ToCombatState();
        }
        
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
    public void ToCombatState()
    {
        aiController.aiRole = AIController.role.Combat;
    }

    public void UpdateState()
    {
        aiController.steeringSeek.target = chaseTarget.position;
        aiController.steeringAlign.target = chaseTarget.rotation.y;
        if (aiController.steeringArrive.enabled || aiController.steeringWander.enabled)
        {
            aiController.steeringArrive.enabled = false;
            aiController.steeringWander.enabled = false;
        }
    }
}
