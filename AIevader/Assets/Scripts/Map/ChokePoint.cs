using UnityEngine;

public class ChokePoint : MonoBehaviour {
    public Transform player;
    [HideInInspector]
    public float distance;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Can change this to likelihood later on
        distance = Mathf.Abs((transform.position - player.transform.position).magnitude);
	}
}
