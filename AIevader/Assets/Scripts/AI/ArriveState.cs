using System;
using UnityEngine;

public class ArriveState : IEnemyState
{
    private AIController aiController;
    public Transform target;

    public ArriveState(AIController aiController)
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
        Debug.Log("Already in arrive state");
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
        aiController.aiRole = AIController.role.Wander;
    }
    public void ToCombatState()
    {
        aiController.aiRole = AIController.role.Combat;
    }

    public void UpdateState()
    {
        if (aiController.steeringArrive.target == null)
        {
            aiController.steeringArrive.target = target.position;
            aiController.steeringAlign.target = target.rotation.y;
        }
    }
}
