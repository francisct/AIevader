using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandexController : MonoBehaviour {

    [SerializeField]
    private float expandedHeight = 1;
    private Vector3 initialScale;
    private Vector3 finalScale;
    [SerializeField]
    private float expansionSpeed = 4.4f;
    private float startTime;
    private float journeyLength;
    private bool expandIntent;
    private bool shrinkIntent;
    
    [SerializeField]
    private float impulseForce = 800;
    [SerializeField]
    private Transform parent;

    private bool expanded;

    Rigidbody target;

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
        //if 
        if (expandIntent)
        {
            target = collision.rigidbody;
            CreateImpulse(target);
        }
        else
            target = collision.rigidbody;
        
    }

    private void OnCollisionExit(Collision collision)
    {
        target = null;
    }

    public void ToggleActivation()
    {
        if (expanded) Deactivate();
        else Activate();
    }

    public void Activate()
    {
        shrinkIntent = false;
        expanded = true;
        startTime = Time.time;
        journeyLength = Vector3.Distance(initialScale, finalScale);
        expandIntent = true;

        if (target) CreateImpulse(target);
    }

    public void Deactivate()
    {
        expandIntent = false;
        expanded = false;
        startTime = Time.time;
        journeyLength = Vector3.Distance(finalScale, initialScale);
        shrinkIntent = true;
    }

    private void CreateImpulse(Rigidbody target)
    {
        target.AddForce(transform.up * impulseForce + Vector3.up * 50, ForceMode.Impulse);
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
