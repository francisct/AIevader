using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandexRemote : MonoBehaviour {
    
    [SerializeField]
    private GameObject expandexPrefab;
    private GameObject deployedExpandex;
    private ExpandexController expandex;
    private bool allowedToTakeExpandex;
    
    
    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        
	}

    private void OnGUI()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!deployedExpandex)
                DropExpandex();
            else if (deployedExpandex && allowedToTakeExpandex)
                PickupExpandex();
        }
        if (Input.GetKeyDown(KeyCode.E))
            Activate();
        if (Input.GetKeyDown(KeyCode.R))
            Deactivate();
    }

    private void Activate()
    {
        if (deployedExpandex)
            expandex.OnActivate();
    }

    private void Deactivate()
    {
        if (deployedExpandex)
            expandex.OnDeactivte();
    }

    private void DropExpandex()
    {
        deployedExpandex = Instantiate(expandexPrefab, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity) as GameObject;
        expandex = GameObject.Find("ScaleFromOneSide").GetComponent<ExpandexController>();
    }

    private void PickupExpandex()
    {
        Destroy(deployedExpandex);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.name == "ScaleFromOneSide")
            allowedToTakeExpandex = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.name == "ScaleFromOneSide")
            allowedToTakeExpandex = false;
    }
}
