﻿using System;
using UnityEngine;

public class CombatState : IEnemyState
{
    private AIController aiController;
    private GameObject player;
    
    private float CD = 1.5f;
    private float timeCounter = 1.5f;
    private float attackRange = 2.5f;
    private float attackDamage = 15.0f;
    private Rigidbody rigidBody;
    public CombatState(AIController aiController)
    {
        timeCounter = CD;
        this.aiController = aiController;
        player = GameObject.FindGameObjectWithTag("Player");
        rigidBody = aiController.GetComponent<Rigidbody>();
    }
    public void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
            aiController.steeringSeek.target = aiController.transform.position;
        }
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
        var direction = player.transform.position - aiController.transform.position;
        
        float angle = Vector3.Angle(aiController.transform.forward, direction);
        float distance = direction.magnitude;
        if(distance > attackRange)
        {
            ToChaseState();
        }
        if (Mathf.Abs(angle) < 60 &&  distance < attackRange && timeCounter > CD){
            timeCounter = 0f;
            var rand = UnityEngine.Random.Range(1, 9);
            if(rand <= 3)
            {
                aiController.animator.SetTrigger("attack1");
                aiController.audioSource.PlayOneShot(aiController.combatSound1);
            }
            else if (rand <= 6)
            { 
                aiController.animator.SetTrigger("attack2");
                aiController.audioSource.PlayOneShot(aiController.combatSound2);

            }
            else if (rand <= 9)
            {
                aiController.animator.SetTrigger("attack3");
                aiController.audioSource.PlayOneShot(aiController.combatSound3);
            }
            direction = player.transform.position - aiController.transform.position;
            distance = direction.magnitude;
            if (distance <= attackRange)
            {
                player.GetComponent<HealthController>().TakeDamage(attackDamage);
            }

        }
        else if (Mathf.Abs(angle) >= 60)
        {
            var rotation = Quaternion.LookRotation(direction);
            aiController.steeringAlign.target = rotation.eulerAngles.y;
        }
        aiController.steeringSeek.target = aiController.transform.position;

        
        timeCounter += Time.deltaTime;
    }
    public void EnableMovement()
    {
        return;
    }
    public void ResetCombatCD()
    {
        timeCounter = CD;
    }
}
