using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour {

    public GameObject ai;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ai.SetActive(true);
        }
    }
}
