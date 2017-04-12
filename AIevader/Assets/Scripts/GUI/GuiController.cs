using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuiController : MonoBehaviour {

    // Use this for initialization
    Text gameOver;
	void Start () {
        gameOver = GameObject.Find("GameOverText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GameOver()
    {
        gameOver.text = "Game Over!";
    }
}
