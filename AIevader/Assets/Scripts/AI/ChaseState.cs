using System;
using UnityEngine;

public class ChaseState : IEnemyState
{
    private Transform chaseTarget;
    private AIController aiController;
    private Rigidbody rigidBody;
    public ChaseState(AIController aiController)
    {
        chaseTarget = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        this.aiController = aiController;
        rigidBody = aiController.GetComponent<Rigidbody>();
    }
    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
            aiController.steeringSeek.target = aiController.transform.position;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
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
        if (aiController.path != null)
        {
            aiController.path.Clear();
        }
        aiController.aiRole = AIController.role.Arrive;
    }

    public void ToChaseState()
    {
        aiController.audioSource.PlayOneShot(aiController.chaseSound);
    }

    public void ToIdleState()
    {
        if (aiController.path != null)
        {
            aiController.path.Clear();
        }
        aiController.aiRole = AIController.role.Idle;
    }

    public void ToWanderState()
    {
        if (aiController.path != null)
        {
            aiController.path.Clear();
        }
        aiController.aiRole = AIController.role.Wander;
    }
    public void ToCombatState()
    {
        if (aiController.path != null)
        {
            aiController.path.Clear();
        }
        aiController.combatState.ResetCombatCD();
        aiController.aiRole = AIController.role.Combat;
    }

    public void UpdateState()
    {

        if (chaseTarget == null)
        {
            return;
        }
        RaycastHit hit;
        var direction = chaseTarget.transform.position - aiController.transform.position;
        if (Physics.Raycast(aiController.transform.position + Vector3.up * 0.5f, direction, out hit) && hit.transform == chaseTarget.transform)
        {
            Debug.Log("JAMAL");
            aiController.steeringSeek.target = chaseTarget.transform.position;
        }
        else {
            Debug.Log("LUIGI");
            aiController.aStar.FindPath(aiController.transform.position, chaseTarget.transform.position);
            if (aiController.path.Count > 0 && (aiController.transform.position - aiController.path[0].worldPosition).magnitude < 0.5f)
            {
                aiController.path.RemoveAt(0);
            }

            if (chaseTarget != null && aiController.path != null && aiController.path.Count > 0)
            {
                aiController.steeringSeek.target = aiController.path[0].worldPosition;
                aiController.steeringSeek.target.y = 0;

            }
        }
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
