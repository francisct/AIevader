using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWander : MonoBehaviour
{

    public int wanderOffset = 6;
    public int wanderRadius = 3;
    private float wanderOrientation = 0.0f;
    private SteeringSeek seek;
    private SteeringAlign align;

    void Start()
    {
        seek = GetComponent<SteeringSeek>();
        align = GetComponent<SteeringAlign>();
    }
    void Update()
    {
        float randomBinomial = Random.value - Random.value;
        wanderOrientation += randomBinomial * wanderRadius;
        float targetOrientation = wanderOrientation
        + transform.eulerAngles.y;
        Vector3 target = transform.position
        + wanderOffset * transform.forward;
        Vector3 ek = (Quaternion.Euler(0, targetOrientation, 0)
        * Vector3.forward) * wanderRadius;
        target += ek;
        seek.target = target;
        align.target = Mathf.Atan2(seek.velocity.x, seek.velocity.z)
        * Mathf.Rad2Deg;
    }
}
