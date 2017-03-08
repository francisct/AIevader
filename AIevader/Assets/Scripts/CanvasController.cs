using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour {
    [SerializeField]
    Text povMode;
    [SerializeField]
    Text heuristicNo;

    public static bool blueWon;
    public static bool redWon;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (OptionsController.povMode) povMode.text = "PoV grid";
        else povMode.text = "Regular grid";

        heuristicNo.text = OptionsController.currentHeuristic;
        
    }
}
