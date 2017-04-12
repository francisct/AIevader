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
        aiController.path.Clear();
        aiController.aiRole = AIController.role.Arrive;
    }

    public void ToChaseState()
    {
        aiController.audioSource.PlayOneShot(aiController.chaseSound);
    }

    public void ToIdleState()
    {
        aiController.path.Clear();
        aiController.aiRole = AIController.role.Idle;
    }

    public void ToWanderState()
    {
        aiController.path.Clear();
        aiController.aiRole = AIController.role.Wander;
    }
    public void ToCombatState()
    {
        aiController.path.Clear();
        aiController.aiRole = AIController.role.Combat;
    }

    public void UpdateState()
    {
        aiController.steeringSeek.target = chaseTarget.position;
        aiController.steeringSeek.target.y = 0;
        aiController.steeringAlign.target = Mathf.Atan2(aiController.steeringSeek.velocity.x, aiController.steeringSeek.velocity.z) * Mathf.Rad2Deg;
        if (aiController.steeringArrive.enabled || aiController.steeringWander.enabled)
        {
            aiController.steeringArrive.enabled = false;
            aiController.steeringWander.enabled = false;
        }
        if (!aiController.steeringSeek.enabled)
        {
            EnableMovement();
        }
    }
    public void EnableMovement()
    {
        aiController.steeringSeek.enabled = true;
    }
}
