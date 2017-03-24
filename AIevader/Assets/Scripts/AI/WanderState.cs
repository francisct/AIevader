using System;
using UnityEngine;

public class WanderState : IEnemyState
{
    private AIController aiController;
    public WanderState(AIController aiController)
    {
        this.aiController = aiController;
    }
    public void OnCollisionEnter(Collision other)
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        
    }

    public void OnTriggerExit(Collider other)
    {
        
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
        aiController.aiRole = AIController.role.Idle;
    }

    public void ToWanderState()
    {
        
    }

    public void ToCombatState()
    {
        aiController.aiRole = AIController.role.Combat;
    }

    public void UpdateState()
    {
        throw new NotImplementedException();
    }
}
