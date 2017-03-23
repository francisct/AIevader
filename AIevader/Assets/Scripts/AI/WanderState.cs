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
        throw new NotImplementedException();
    }

    public void OnTriggerEnter(Collider other)
    {
        throw new NotImplementedException();
    }

    public void OnTriggerExit(Collider other)
    {
        throw new NotImplementedException();
    }

    public void ToArriveState()
    {
        throw new NotImplementedException();
    }

    public void ToChaseState()
    {
        throw new NotImplementedException();
    }

    public void ToIdleState()
    {
        throw new NotImplementedException();
    }

    public void ToWanderState()
    {
        throw new NotImplementedException();
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
