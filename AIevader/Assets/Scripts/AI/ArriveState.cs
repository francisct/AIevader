using System;
using UnityEngine;

public class ArriveState : IEnemyState
{
    private AIController aiController;
    public ChokePoint target;

    public ArriveState(AIController aiController)
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
        ;
    }

    public void ToChaseState()
    {
        AIManager.FreeChokePoint(target.GetComponent<ChokePoint>());
        aiController.aiRole = AIController.role.Chase;
    }

    public void ToIdleState()
    {
        aiController.aiRole = AIController.role.Idle;
    }

    public void ToWanderState()
    {
        AIManager.FreeChokePoint(target.GetComponent<ChokePoint>());
        aiController.aiRole = AIController.role.Wander;
    }
    public void ToCombatState()
    {
        AIManager.FreeChokePoint(target.GetComponent<ChokePoint>());
        aiController.aiRole = AIController.role.Combat;
    }

    public void UpdateState()
    {
        if (target != null)
        {
            aiController.steeringArrive.target = target.transform.position;
            aiController.steeringAlign.target = target.transform.rotation.y;
        }
        if(target.transform.position == aiController.transform.position)
        {
            ToIdleState();
        }
    }
}
