using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWander : MonoBehaviour
{

    public float wanderRange = 20f;
    public float wanderRadius = 3f;
    private SteeringSeek seek;
    private SteeringAlign align;
    private AIController aiController;
    private float directionCD = 5f;
    private float timeCounter = 0f;
    float direction = 0f;

    void Start()
    {
        seek = GetComponent<SteeringSeek>();
        align = GetComponent<SteeringAlign>();
        aiController = GetComponent<AIController>();
    }
    void Update()
    {
        if(timeCounter > directionCD)
        {
            direction = Random.Range(-1f, 1f);
            timeCounter = 0;
        }
        timeCounter += Time.deltaTime;
        var foundDirection = false;
        var offset = 10f;
        var currentDirectionAdjustment = 0f;
        Vector3 target = Vector3.zero;
        while (!foundDirection)
        {
            if(currentDirectionAdjustment > 400)
            {
                return;
            }
            Vector3 wanderRadiusPosition = Quaternion.Euler(0, 1 * (direction * currentDirectionAdjustment) , 0) * transform.forward.normalized * wanderRange + transform.position;
            var targetX = Random.Range(-wanderRadius, wanderRadius);
            var targetZ = Random.Range(-wanderRadius, wanderRadius);
            target = new Vector3(targetX, transform.position.y, targetZ);
            if (target.magnitude > wanderRadius)
            {
                target = target.normalized * wanderRadius;
            }
            target += wanderRadiusPosition;
            var dir = new Vector3(target.x, transform.position.y, target.z) - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, dir, out hit, dir.magnitude))
            {
                if (hit.transform.gameObject.tag == "Player")
                {
                    foundDirection = true;
                    aiController.currentState.ToChaseState();
                }
                currentDirectionAdjustment += offset;
            }
            else
            {
                foundDirection = true;
            }
        }
        seek.target = new Vector3(target.x, 0, target.z);
        Vector3 targetDirection = target - transform.position;
        var rotation = Quaternion.LookRotation(targetDirection);
        align.target = rotation.eulerAngles.y;

    }
}
