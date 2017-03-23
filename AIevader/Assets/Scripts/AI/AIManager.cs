using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public AIController[] AIs;
    private List<AIController> commandableAIs = new List<AIController>();
    private List<AIController> busyAIs = new List<AIController>();
    private GameObject player;

    // Use this for initialization
    void Start()
    {
        InitializeLists();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void InitializeLists()
    {
        commandableAIs.AddRange(AIs);
    }
    /// <summary>
    /// Mark an AI as busy
    /// </summary>
    /// <param name="ai"></param>
    public void OccupiedAI(AIController ai)
    {
        commandableAIs.Remove(ai);
        busyAIs.Add(ai);
    }
    /// <summary>
    /// Mark an AI as commandable
    /// </summary>
    /// <param name="ai"></param>
    public void FreeAI(AIController ai)
    {
        busyAIs.Remove(ai);
        commandableAIs.Add(ai);
    }
    /// <summary>
    /// Find the closest available AI to Gameobject g
    /// </summary>
    /// <param name="g"></param>
    /// <returns></returns>
    private AIController FindClosestAvailableAI(GameObject g)
    {
        AIController closestAI = null;
        float closestDistance = Mathf.Infinity;

        foreach (AIController ai in commandableAIs)
        {

            float distance = (ai.transform.position - g.transform.position).magnitude;
            if (distance < closestDistance)
            {
                closestAI = ai;
            }

        }

        return closestAI;
    }

    private void SendAIToChase(AIController ai)
    {
        OccupiedAI(ai);
        ai.aiRole = AIController.role.Chase;
    }
}
