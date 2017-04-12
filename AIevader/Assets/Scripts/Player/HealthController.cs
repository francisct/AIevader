using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    [SerializeField]
    private int health;
    private int initialHealth;
    private GUIBarScript healthBar;
    private GuiController gui;
	// Use this for initialization
	void Start () {
        initialHealth = health;
        healthBar = GameObject.Find("HealthBar").GetComponent<GUIBarScript>();
        gui = GameObject.Find("Canvas").GetComponent<GuiController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(float damage)
    {
        health -= (int)damage;
        healthBar.Value = health / initialHealth;
        if (health <= 0) gui.GameOver();
    }

    public void RestoreHealth(int restored)
    {
        if (health + restored <= initialHealth)
            health += restored;
        else health = initialHealth;
    }
}
