using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {
    public AIController[] AIs;
    private List<AIController> commandableAIs = new List<AIController>();
    private List<AIController> busyAIs = new List<AIController>();
    private GameObject player;

    // Use this for initialization
    void Start () {
        InitializeLists();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
		
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
}
