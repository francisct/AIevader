﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    [SerializeField]
    private int health;
    private int initialHealth;
    private GUIBarScript healthBar;
    public GuiController gui;
	// Use this for initialization
	void Start () {
        initialHealth = health;
        healthBar = GameObject.Find("HealthBar").GetComponent<GUIBarScript>();
	}
	
	// Update is called once per frame
	void Update () {
        healthBar.Value = (float)health / 100;
    }

    public void TakeDamage(float damage)
    {
        health -= (int)damage;
        if (health <= 0) gui.GameOver();
    }

    public void RestoreHealth(int restored)
    {
        if (health + restored <= initialHealth)
            health += restored;
        else health = initialHealth;
    }
}
