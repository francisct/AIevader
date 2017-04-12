﻿using System;
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
        aiController.path.Clear();
        aiController.audioSource.PlayOneShot(aiController.chaseSound);
        AIManager.FreeChokePoint(target.GetComponent<ChokePoint>());
        aiController.aiRole = AIController.role.Chase;
    }

    public void ToIdleState()
    {
        aiController.path.Clear();
        aiController.aiRole = AIController.role.Idle;
    }

    public void ToWanderState()
    {
        aiController.path.Clear();
        AIManager.FreeChokePoint(target.GetComponent<ChokePoint>());
        aiController.aiRole = AIController.role.Wander;
    }
    public void ToCombatState()
    {
        aiController.path.Clear();
        AIManager.FreeChokePoint(target.GetComponent<ChokePoint>());
        aiController.aiRole = AIController.role.Combat;
    }

    public void UpdateState()
    {
        if(target == null)
        {
            return;
        }
        if ((aiController.transform.position - aiController.path[0].worldPosition).magnitude < 0.5f)
        {
            aiController.path.RemoveAt(0);
        }
        if (aiController.path.Count <= 1)
        {
            if (aiController.steeringSeek.enabled || aiController.steeringWander.enabled)
            {
                aiController.steeringSeek.enabled = false;
                aiController.steeringWander.enabled = false;
            }
            if (!aiController.steeringArrive.enabled)
            {
                aiController.steeringArrive.enabled = true;
            }
        }
        else if(aiController.path.Count > 1)
        {
            if(aiController.steeringArrive.enabled || aiController.steeringWander.enabled)
            {
                aiController.steeringArrive.enabled = false;
                aiController.steeringWander.enabled = false;
            }
            if (!aiController.steeringSeek.enabled)
            {
                aiController.steeringSeek.enabled = true;
            }
        }
        if (target != null && aiController.path != null)
        {
            SetPathTarget(aiController.path[0].worldPosition);
            AdjustAlignment();
        }
        if(Mathf.Abs((target.transform.position - aiController.transform.position).magnitude) < aiController.steeringAlign.targetRadius)
        {
            ToIdleState();
        }
    }
    public void EnableMovement()
    {
        if (aiController.path.Count <= 1)
        {
            aiController.steeringArrive.enabled = true;
        }
        else if (aiController.path.Count > 1)
        {
            aiController.steeringSeek.enabled = true;
        }
    }
    void SetPathTarget(Vector3 nextPosition)
    {
        if (aiController.path.Count <= 1)
        {
            aiController.steeringArrive.target = nextPosition;
        }
        else if (aiController.path.Count > 1)
        {
            aiController.steeringSeek.target = nextPosition;
        }
    }
    public void SendAIToNewChokePoint(ChokePoint target) {
        this.target = target;
        aiController.aStar.FindPath(aiController.transform.position, target.transform.position);
    }
    void AdjustAlignment()
    {
        if (aiController.path.Count <= 1)
        {
            aiController.steeringAlign.target = Mathf.Atan2(aiController.steeringArrive.velocity.x, aiController.steeringArrive.velocity.z) * Mathf.Rad2Deg;
        }
        else if (aiController.path.Count > 1)
        {
            aiController.steeringAlign.target = Mathf.Atan2(aiController.steeringSeek.velocity.x, aiController.steeringSeek.velocity.z) * Mathf.Rad2Deg;
            aiController.steeringArrive.velocity = aiController.steeringSeek.velocity;
        }
    }
}
