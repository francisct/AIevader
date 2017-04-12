using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public GameObject chokePoints;
    private static List<ChokePoint> availableChokePoints = new List<ChokePoint>();
    private static List<ChokePoint> occupiedChokePoints = new List<ChokePoint>();
    private static List<AIController> commandableAIs = new List<AIController>();
    private static Dictionary<ChokePoint, AIController> chokePointAIs = new Dictionary<ChokePoint, AIController>();
    private static List<AIController> busyAIs = new List<AIController>();
    private static GameObject player;
    private static int currentAIsAlive;

    void Awake()
    {
        foreach(Transform child in transform)
        {
            if (child.name != "MonsterSpawner")
            {
                child.gameObject.SetActive(false);
            }
        }
    }

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
        availableChokePoints.Sort((x, y) => x.distance.CompareTo(y.distance));
        UpdateChokePointAI();
    }
    private void InitializeLists()
    {
        foreach(Transform t in transform)
        {
            commandableAIs.Add(t.GetComponent<AIController>());
        }
        foreach(Transform t in chokePoints.transform)
        {
            availableChokePoints.Add(t.GetComponent<ChokePoint>());
        }
    }

    private void AssignRole(AIController ai)
    {
        if (!IsThereAnAIChasing())
        {
            SendAIToChase(ai);
        }
        else if (availableChokePoints.Count > 0)
        {
            var chokePoint = FindBestChokePoint();
            if (chokePoint)
            {
                SendAIToArrive(ai, chokePoint);
            }
        }
        else
        {
            SendAIToChase(ai);
        }
    }

    /// <summary>
    /// Mark an AI as busy
    /// </summary>
    /// <param name="ai"></param>
    public static void OccupiedAI(AIController ai)
    {
        if (commandableAIs.Contains(ai))
        {
            commandableAIs.Remove(ai);
        }
        if (!busyAIs.Contains(ai))
        {
            busyAIs.Add(ai);
        }
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
    public static void OccupiedChokePoint(ChokePoint cp)
    {
        occupiedChokePoints.Add(cp);
        availableChokePoints.Remove(cp);
    }
    public static void FreeChokePoint(ChokePoint cp)
    {
        occupiedChokePoints.Remove(cp);
        availableChokePoints.Add(cp);
    }
    public static void FreeChokePointFrinAI(AIController ai)
    {
        var cp = chokePointAIs.FirstOrDefault(x => x.Value == ai).Key;
        occupiedChokePoints.Remove(cp);
        availableChokePoints.Add(cp);
    }
    /// <summary>
    /// Find the closest available AI to Transform t
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    private AIController FindClosestAvailableAI(Transform t)
    {
        AIController closestAI = null;
        float closestDistance = Mathf.Infinity;

        foreach (AIController ai in commandableAIs)
        {

            float distance = (ai.transform.position - t.position).magnitude;
            if (distance < closestDistance)
            {
                closestAI = ai;
                closestDistance = distance;
            }

        }

        return closestAI;
    }

    private bool IsThereAnAIChasing()
    {
        foreach (AIController ai in busyAIs)
        {
            if (ai.aiRole == AIController.role.Chase || ai.aiRole == AIController.role.Combat)
            {
                return true;
            }
        }
        return false;
    }

    public static void AIKilled(AIController ai)
    {
        ai.animator.SetTrigger("die");
        commandableAIs.Remove(ai);
        busyAIs.Remove(ai);
        currentAIsAlive--;
    }

    private void SendAIToChase(AIController ai)
    {
        OccupiedAI(ai);
        ai.currentState.ToChaseState();
    }

    private void SendAIToWander(AIController ai)
    {
        FreeAI(ai);
        ai.aiRole = AIController.role.Wander;
    }

    private void SendAIToIdle(AIController ai)
    {
        OccupiedAI(ai);
        ai.aiRole = AIController.role.Idle;
    }

    private void SendAIToArrive(AIController ai, ChokePoint target)
    {
        OccupiedAI(ai);
        chokePointAIs[target] = ai;
        OccupiedChokePoint(target);
        ai.aiRole = AIController.role.Arrive;
        ai.arriveState.SendAIToNewChokePoint(target);
    }

    private void SendAIToCombat(AIController ai)
    {
        OccupiedAI(ai);
        ai.aiRole = AIController.role.Combat;
    }
    public void ReportSawPlayer(AIController ai)
    {
        AssignRole(ai);
    }
    private ChokePoint FindBestChokePoint()
    {
        if (availableChokePoints[0])
        {
            return availableChokePoints[0];
        }
        return null;
    }
    private void UpdateChokePointAI()
    {
        if (availableChokePoints.Count > 0)
        {
            List<ChokePoint> keys = new List<ChokePoint>(chokePointAIs.Keys);
            foreach (ChokePoint key in keys)
            {
                var bestChokePoint = FindBestChokePoint();
                if (bestChokePoint && bestChokePoint.distance < key.distance)
                {
                    var tmpAI = chokePointAIs[key];
                    SendAIToArrive(tmpAI, bestChokePoint);
                    chokePointAIs.Remove(key);
                    FreeChokePoint(key);

                }
            }
        }

    }
}
