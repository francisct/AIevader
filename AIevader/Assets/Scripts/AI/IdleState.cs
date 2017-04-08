using System;
using UnityEngine;

public class IdleState : IEnemyState
{
    private AIController aiController;
    public IdleState(AIController aiController)
    {
        this.aiController = aiController;
    }
    public void OnCollisionEnter(Collision other)
    {
        ;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ToChaseState();
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
        aiController.aiRole = AIController.role.Chase;
    }

    public void ToIdleState()
    {
        ;
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
        
    }
}
