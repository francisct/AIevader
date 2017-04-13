using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public GameObject winPanel;
    public AIManager AIManager;
    public GameObject winWall;

    private bool canLeave;

    void Start()
    {
        winWall = GameObject.Find("WinWall");
    }

    void Update()
    {
        if(AIManager.currentAIsAlive <= 7)
        {
            canLeave = true;
            Destroy(winWall);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
                Time.timeScale = 0;
                winPanel.SetActive(true);
        }
    }
}
