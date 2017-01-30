using UnityEngine;
using System.Collections;

public class PlayerUnit : MonoBehaviour
{
    NavMeshAgent navMeshAgent;

    void Start ()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 location)
    {
        navMeshAgent.SetDestination(location);
    }
}
