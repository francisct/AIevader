﻿using System;
using UnityEngine;

public class WanderState : IEnemyState
{
    private AIController aiController;
    private GameObject player;
    private float visibilityRange = 200f;
    private float visibilityAngle = 45f;
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
        if (other.tag == "Player")
        {
            aiController.aiManager.ReportSawPlayer(aiController);
        }
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
        aiController.audioSource.PlayOneShot(aiController.chaseSound);
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
        var dir = player.transform.position - currentPos;
        dir.y = 0;
        RaycastHit hit;
        if (Physics.Raycast(currentPos + Vector3.up * 0.5f, dir, out hit) && hit.transform == player.transform)
        {
            var direction = player.transform.position - aiController.transform.position;
            float angle = Vector3.Angle(aiController.transform.forward, direction);
            if (Mathf.Abs(angle) < visibilityAngle)
            {
                aiController.aiManager.ReportSawPlayer(aiController);
            }
        }
        if (aiController.steeringArrive.enabled)
        {
            aiController.steeringArrive.enabled = false;
        }
        if (!aiController.steeringWander.enabled || !aiController.steeringSeek.enabled)
        {
            EnableMovement();
        }
    }
    public void EnableMovement()
    {
        aiController.steeringWander.enabled = true;
        aiController.steeringSeek.enabled = true;
    }
}
