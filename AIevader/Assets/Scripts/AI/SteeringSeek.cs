﻿using UnityEngine;

public class SteeringSeek : MonoBehaviour
{

    public Vector3 target;
    public Vector3 velocity;
    public float maxAcceleration = 45;
    public float maxSpeed = 25;
    private AIController aiController;
    void Start()
    {
        target = transform.position;
        aiController = GetComponent<AIController>();
    }
    void Update()
    {
        Vector3 linear = target - transform.position;
        if (linear.magnitude <= 0.01f)
        {
            aiController.velocity = Vector3.zero;
            return;
        }
        linear.Normalize();
        linear *= maxAcceleration;
        transform.position += velocity * Time.deltaTime;
        velocity += linear * Time.deltaTime;
        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }
        aiController.velocity = velocity;
    }
}
