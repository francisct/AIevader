using UnityEngine;
using System.Collections;

public class OptionsController : MonoBehaviour
{

    public static bool povMode = false;
    public static int heuristicNo = 0;
    private int heuristicCtr = 2;
    private static string[] heuristicNames = new string[] { "Euclidean distance", "Dijkstra", "Cluster"};
    public static string currentHeuristic;

    [SerializeField]
    private Instantiator instantiator;

    private void Start()
    {
        currentHeuristic = heuristicNames[heuristicNo];
    }

    void Update()
    {
        if (Input.GetButtonDown("optionPov"))
        {
            povMode = !povMode;
            instantiator.generateNodes();
        }
        else if (Input.GetButtonDown("optionHeuristic"))
        {
            if (heuristicNo < heuristicCtr) heuristicNo++;
            else heuristicNo = 0;

            currentHeuristic = heuristicNames[heuristicNo];
        }
    }
}

