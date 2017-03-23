﻿using System;
using UnityEngine;

public class CombatState : IEnemyState
{
    private AIController aiController;
    public CombatState(AIController aiController)
    {
        this.aiController = aiController;
    }
    public void OnCollisionEnter(Collision other)
    {
        ;
    }

    public void OnTriggerEnter(Collider other)
    {
        ;
    }

    public void OnTriggerExit(Collider other)
    {
        ToChaseState();
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
        aiController.aiRole = AIController.role.Wander;
    }

    public void ToCombatState()
    {
        ;
    }

    public void UpdateState()
    {
        ;//combat stuff here
    }
}
