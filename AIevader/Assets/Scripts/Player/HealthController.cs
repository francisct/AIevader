using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    [SerializeField]
    private int health;
    private int initialHealth;
    private GUIBarScript healthBar;
	// Use this for initialization
	void Start () {
        initialHealth = health;
        healthBar = GameObject.Find("HealthBar").GetComponent<GUIBarScript>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    bool TakeDamage(int damage)
    {
        health -= damage;
        healthBar.Value = health / initialHealth;
        return health <= 0;
    }
}
