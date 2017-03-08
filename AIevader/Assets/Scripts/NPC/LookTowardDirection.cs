using UnityEngine;
using System.Collections;

public class LookTowardDirection : MonoBehaviour {
    protected Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DoLookTowardDirection(rb.velocity);
    }

    public void DoLookTowardDirection(Vector3 direction)
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