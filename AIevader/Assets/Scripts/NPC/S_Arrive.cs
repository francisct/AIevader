using UnityEngine;
using System.Collections;

public class S_Arrive : MonoBehaviour {
    
    float acceleration = 2.0f;

    protected Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 nextStep, float speed)
    {
        Vector3 toNextStep = nextStep - transform.position;
        float mag = toNextStep.magnitude;
        float clampedSpeed = Mathf.Clamp(mag * acceleration, 0.0F, speed);
        rb.velocity = (toNextStep.normalized * clampedSpeed);
    }
}
