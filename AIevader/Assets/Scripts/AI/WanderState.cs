using System;
using UnityEngine;

public class WanderState : IEnemyState
{
    private AIController aiController;
    private GameObject player;

    public WanderState(AIController aiController)
    {
        this.aiController = aiController;
        player = GameObject.FindGameObjectWithTag("Player");
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
        var currentPos = aiController.transform.position;
        var dir = player.transform.position - currentPos + Vector3.up*0.5f;
        RaycastHit hit;
        var a = Physics.Raycast(currentPos, dir, out hit);
        if (Physics.Raycast(currentPos, dir, out hit) && hit.transform == player.transform)
        {
            aiController.aiManager.ReportSawPlayer(aiController);
        }
        if (aiController.steeringArrive.enabled)
        {
            aiController.steeringArrive.enabled = false;
        }
        if (!aiController.steeringWander.enabled || !aiController.steeringSeek.enabled)
        {
            aiController.steeringWander.enabled = true;
            aiController.steeringSeek.enabled = true;
        }
    }
}
