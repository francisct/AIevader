using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
    
    float acceleration = 2.0f;

    Vector3 oldVelocity;
    protected Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        oldVelocity = rb.velocity;
    }

    public void Seek(Vector3 nextStep, float speed)
    {
        //move is only active if there are no external forces applied
        if (!externalForceApplied())
        {
            Vector3 toNextStep = nextStep - transform.position;
            float mag = toNextStep.magnitude;
            float clampedSpeed = Mathf.Clamp(mag * acceleration, 0.0F, speed);
            rb.velocity = (toNextStep.normalized * speed);
            oldVelocity = rb.velocity;
            LookTowardDirection(rb.velocity);
        }
        else oldVelocity = rb.velocity;
    }

    private bool externalForceApplied()
    {
        return rb.velocity != oldVelocity;
    }

    private void LookTowardDirection(Vector3 direction)
    {
        Vector3 rotToward = direction.normalized;
        if (rotToward.x != 0 || rotToward.y != 0 || rotToward.z != 0)
        {
            Debug.Log(rotToward);
            Quaternion lookRotation = Quaternion.LookRotation(rotToward);
            transform.localRotation = lookRotation;
        }
    }
}
