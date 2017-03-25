using System.Collections;
using UnityEngine;

public class PathFollowing : MonoBehaviour
{

    private SteeringAlign align;
    private SteeringSeek seek;
    private ArrayList targets;
    private int currentid = 0;
    void Start()
    {
        align = GetComponent<SteeringAlign>();
        seek = GetComponent<SteeringSeek>();
    }

    void Update()
    {
        Vector3 distance = transform.position -
        (Vector3)targets[currentid];
        if (distance.magnitude < 0.5)
            currentid = (currentid + 1) % targets.Count;
        seek.target = (Vector3)targets[currentid];
        align.target =
        Mathf.Atan2(seek.velocity.x, seek.velocity.z)
        * Mathf.Rad2Deg;
    }
}
