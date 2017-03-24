using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public static AIController[] AIs;
    private static List<AIController> commandableAIs = new List<AIController>();
    private static List<AIController> busyAIs = new List<AIController>();
    private static GameObject player;
    private static int currentAIsAlive;

    // Use this for initialization
    void Start()
    {
        InitializeLists();
        currentAIsAlive = commandableAIs.Count;
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
    public static void OccupiedAI(AIController ai)
    {
        commandableAIs.Remove(ai);
        busyAIs.Add(ai);
    }
    /// <summary>
    /// Mark an AI as commandable
    /// </summary>
    /// <param name="ai"></param>
    public static void FreeAI(AIController ai)
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
                closestDistance = distance;
            }

        }

        return closestAI;
    }

    public static void AIKilled(AIController ai)
    {
        commandableAIs.Remove(ai);
        busyAIs.Remove(ai);
        currentAIsAlive--;
    }

    private void SendAIToChase(AIController ai)
    {
        OccupiedAI(ai);
        ai.aiRole = AIController.role.Chase;
    }

    private void SendAIToWander(AIController ai)
    {
        OccupiedAI(ai);
        ai.aiRole = AIController.role.Wander;
    }

    private void SendAIToIdle(AIController ai)
    {
        OccupiedAI(ai);
        ai.aiRole = AIController.role.Idle;
    }

    private void SendAIToArrive(AIController ai)
    {
        OccupiedAI(ai);
        ai.aiRole = AIController.role.Arrive;
    }

    private void SendAIToCombat(AIController ai)
    {
        OccupiedAI(ai);
        ai.aiRole = AIController.role.Combat;
    }
}
