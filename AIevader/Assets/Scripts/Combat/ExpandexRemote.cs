using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandexRemote : MonoBehaviour {
    
    [SerializeField]
    private GameObject expandexPrefab;
    private GameObject expandexClone;
    private ExpandexController expandex;
    private bool takeExpandex;
    private bool hasExpandex;
    
    // Use this for initialization
    void Start () {
        hasExpandex = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (hasExpandex)
                DropExpandex();
            else if(!hasExpandex && takeExpandex)
                PickupExpandex();
        }
        if (Input.GetKeyDown(KeyCode.E))
            Activate();
        if (Input.GetKeyDown(KeyCode.R))
            Deactivate();
	}

    private void Activate()
    {
        if (!hasExpandex)
            expandex.OnActivate();
    }

    private void Deactivate()
    {
        if (!hasExpandex)
            expandex.OnDeactivte();
    }

    private void DropExpandex()
    {
        expandexClone = Instantiate(expandexPrefab, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity) as GameObject;
        expandex = GameObject.Find("ScaleFromOneSide").GetComponent<ExpandexController>();
        hasExpandex = false;
    }

    private void PickupExpandex()
    {
        Destroy(expandexClone);
        hasExpandex = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.name == "ScaleFromOneSide")
            takeExpandex = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.name == "ScaleFromOneSide")
            takeExpandex = false;
    }
}
