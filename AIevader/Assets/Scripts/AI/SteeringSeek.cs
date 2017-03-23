﻿using UnityEngine;

public class SteeringSeek : MonoBehaviour {

    public Vector3 target;
    public Vector3 velocity;
    public int maxAcceleration = 45;
    public int maxSpeed = 25;
    void Start() { target = transform.position; }
    void Update()
    {
        Vector3 linear = target - transform.position;
        if (linear.magnitude == 0) return;
        linear.Normalize();
        linear *= maxAcceleration;
        transform.position += velocity * Time.deltaTime;
        velocity += linear * Time.deltaTime;
        if (velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity *= maxSpeed;
        }
    }
}