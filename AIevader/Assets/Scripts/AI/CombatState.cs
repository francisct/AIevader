using System;
using UnityEngine;

public class CombatState : IEnemyState
{
    private AIController aiController;
    private GameObject player;
    private float timeCounter = 0f;
    private float CD = 1.5f;
    private float attackRange = 2.5f;

    public CombatState(AIController aiController)
    {
        this.aiController = aiController;
        player = GameObject.FindGameObjectWithTag("Player");
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
        var direction = player.transform.position - aiController.transform.position;
        
        float angle = Vector3.Angle(aiController.transform.forward, direction);
        float distance = direction.magnitude;

        if (Mathf.Abs(angle) < 60 &&  distance < attackRange && timeCounter > CD){
            timeCounter = 0f;
            var rand = UnityEngine.Random.Range(1, 9);
            if(rand <= 3)
            {
                aiController.animator.SetTrigger("attack1");
            }
            else if (rand <= 6)
            { 
                aiController.animator.SetTrigger("attack2");

            }
            else if (rand <= 9)
            {
                aiController.animator.SetTrigger("attack3");
            }
            direction = player.transform.position - aiController.transform.position;
            distance = direction.magnitude;
            if (distance <= attackRange)
            {
                //player get hit
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
}
