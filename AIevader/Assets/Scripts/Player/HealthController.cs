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

    public bool TakeDamage(float damage)
    {
        health -= (int)damage;
        healthBar.Value = health / initialHealth;
        return health <= 0;
    }

    public void RestoreHealth(int restored)
    {
        if (health + restored <= initialHealth)
            health += restored;
        else health = initialHealth;
    }
}
