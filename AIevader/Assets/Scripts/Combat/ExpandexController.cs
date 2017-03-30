using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandexController : MonoBehaviour {

    [SerializeField]
    private float expandedHeight;
    private Vector3 initialScale;
    private Vector3 finalScale;
    [SerializeField]
    private float expansionSpeed;
    private float startTime;
    private float journeyLength;
    private bool expandIntent;
    private bool shrinkIntent;
    
    [SerializeField]
    private float impulseForce;
    [SerializeField]
    private Transform parent;

    Rigidbody onTop;

    // Use this for initialization
    void Start () {
        initialScale = parent.localScale;
        finalScale = parent.localScale;
        finalScale.y = expandedHeight;
    }
	
	// Update is called once per frame
	void Update () {
        if (expandIntent) Expand();
        else if (shrinkIntent) Shrink();
	}

    private void OnCollisionEnter(Collision collision)
    {
        onTop = collision.rigidbody;
        OnActivate();
    }

    public void OnActivate()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(initialScale, finalScale);
        expandIntent = true;

        if (onTop) CreateImpulse(onTop);
    }

    public void onDeactivte()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(finalScale, initialScale);
    }

    private void CreateImpulse(Rigidbody onTop)
    {
        onTop.AddForce(new Vector3(0, 1, 0) * impulseForce, ForceMode.Impulse);
    }

    private void Expand()
    {
        float distCovered = (Time.time - startTime) * expansionSpeed;
        float fracJourney = distCovered / journeyLength;
        parent.localScale = Vector3.Lerp(initialScale, finalScale, fracJourney);

        if (parent.localScale == finalScale) expandIntent = false;
    }

    private void Shrink()
    {
        float distCovered = (Time.time - startTime) * expansionSpeed;
        float fracJourney = distCovered / journeyLength;
        parent.localScale = Vector3.Lerp(finalScale, initialScale, fracJourney);

        if (parent.localScale == initialScale) shrinkIntent = false;
    }
}
