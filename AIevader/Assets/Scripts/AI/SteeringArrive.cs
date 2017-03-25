using UnityEngine;

public class SteeringArrive : MonoBehaviour
{
    public Vector3 target;
    public Vector3 velocity;
    public int maxAcceleration = 2;
    public int maxSpeed = 4;
    public float targetRadius = 0.1f;
    public float slowRadius = 5.0f;
    public float timeToTarget = 0.1f;
    void Start()
    {
        target = transform.position;
    }

    void Update()
    {
        Vector3 direction = target - transform.position;
        float distance = direction.magnitude;
        if (distance < targetRadius) return;
        float targetSpeed = 0;
        if (distance > slowRadius) targetSpeed = maxSpeed;
        else targetSpeed = maxSpeed * distance / slowRadius;
        Vector3 targetVelocity = direction;
        targetVelocity.Normalize();
        targetVelocity *= targetSpeed;

        Vector3 linear = targetVelocity - velocity;
        linear /= timeToTarget;
        if (linear.magnitude > maxAcceleration)
        {
            linear.Normalize();
            linear *= maxAcceleration;
        }
        transform.position += velocity * Time.deltaTime;
        velocity += linear * Time.deltaTime;
        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }
    }
}