using UnityEngine;

public class SteeringAlign : MonoBehaviour
{

    public float target;
    public float speed;
    public int maxAngularAcceleration = 1;
    public int maxRotationSpeed = 15;
    public float targetRadius = 0.4f;
    public float slowRadius = 5.0f;
    public float timeToTarget = 0.1f;
    // Use this for initialization
    void Start()
    {
        target = transform.rotation.eulerAngles.y;
    }

    void Update()
    {
        float angle = transform.eulerAngles.y;
        float rotation = Mathf.DeltaAngle(angle, target);
        float rotationSize = Mathf.Abs(rotation);
        if (rotationSize < targetRadius) return;
        float targetRotation = 0.0f;
        if (rotationSize > slowRadius)
            targetRotation = maxRotationSpeed;
        else
            targetRotation = maxRotationSpeed * rotationSize / slowRadius;
        targetRotation *= rotation / rotationSize;
        float angular = targetRotation - speed;
        angular /= timeToTarget;
        float angularAcceleration = Mathf.Abs(angular);
        if (angularAcceleration > maxAngularAcceleration)
        {
            angular /= angularAcceleration;
            angular *= maxAngularAcceleration;
        }
        speed += angular;
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}
