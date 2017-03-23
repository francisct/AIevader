using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour {
    //Todo: Change this to Node instead of vector position
    public Transform target;
    private List<Vector3> path;
    public float rotationSpeed = 5;
    public float maxVelocity = 5;
    public float maxAcceleration = 2;
    public float stoppingRadius = 0.5f;
    public float slowDownRadius = 2;
    public float t2t = 2;
    public bool arrivedAtLocation;
    private Vector3 direction;
    private Vector3 velocity;
    private Vector3 acceleration;
    // Use this for initialization
    void Start () {
        //TODO: Remove this when we have a*
        path = new List<Vector3>();
        path.Add(transform.position);
        path.Add(target.position);

    }
	
	// Update is called once per frame
	void Update () {
        CalculateAcceleration();
        CalculateVelocity();
        CalculatePosition();
    }
    /// <summary>
    /// Calculate acceleration for the grid mode
    /// </summary>
    void CalculateAcceleration()
    {
        if (path.Count <= 0)
        {
            acceleration = Vector3.zero;
            return;
        }
        var deltaP = path[0] - transform.position;
        if (deltaP.magnitude > 0)
        {
            acceleration = maxAcceleration * (deltaP / deltaP.magnitude);
        }
        else
        {
            acceleration = Vector3.zero;
        }
    }
    /// <summary>
    /// Calculate velocity for the grid mode
    /// </summary>
    void CalculateVelocity()
    {
        if (path.Count <= 0)
        {
            velocity = Vector3.zero;
            return;
        }

        direction.Normalize();
        velocity = velocity + acceleration * Time.deltaTime;
        if (velocity.magnitude > maxVelocity)
        {
            velocity = velocity.normalized * maxVelocity;
        }
        var deltaP = target.transform.position - transform.position;
        if (deltaP.magnitude <= slowDownRadius)
        {
            var vDirection = velocity.normalized;
            var v = Mathf.Min(velocity.magnitude, deltaP.magnitude / t2t);
            velocity = v * vDirection;
        }
        SlowlyRotate(velocity);
    }
    /// <summary>
    /// Calculate position for the grid mode
    /// </summary>
    void CalculatePosition()
    {
        if (path.Count <= 0)
        {
            arrivedAtLocation = true;
            return;
        }
        arrivedAtLocation = false;
        var deltaP = path[0] - transform.position;
        if (transform.position == path[0] || deltaP.magnitude <= stoppingRadius || (path.Count > 1 && deltaP.magnitude <= slowDownRadius))
        {
            path.RemoveAt(0);
        }

        transform.position += velocity * Time.deltaTime;
    }
    /// <summary>
    /// Slowly rotate toward a direction
    /// </summary>
    /// <param name="direction"></param>
    void SlowlyRotate(Vector3 direction)
    {
        var q = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotationSpeed * Time.deltaTime);
    }
}
