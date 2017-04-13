using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandexRemote : MonoBehaviour
{

    [SerializeField]
    private GameObject expandexPrefab, expandexPrefab2;
    private GameObject deployedExpandex, deployedExpandex2;
    private ExpandexController [] expandex = new ExpandexController [2];
    private bool allowedToTakeExpandex1, allowedToTakeExpandex2;
    private bool canCreateExpandex1 = true;
    private bool canCreateExpandex2 = true;
    [SerializeField]
    private Transform camera;
    [SerializeField]
    private float maxDistanceToStick;
    [SerializeField]
    LayerMask stickableSurface;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!deployedExpandex || !deployedExpandex2)
                StickExpandex();
            else if (deployedExpandex || deployedExpandex2 && allowedToTakeExpandex1 || allowedToTakeExpandex2)
                PickupExpandex();
        }
        else if (Input.GetKeyDown(KeyCode.E))
            ToggleExpandexActivation();

    }


    private void ToggleExpandexActivation()
    {
        if (deployedExpandex)
            if(expandex[0])
                expandex[0].ToggleActivation();
        if(deployedExpandex2)
            if (expandex[1])
                expandex[1].ToggleActivation();     
    }
    

    private void StickExpandex()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistanceToStick, stickableSurface))
        {
            Vector3 position = hit.point;

            if(canCreateExpandex1)
            {
                deployedExpandex = Instantiate(expandexPrefab, position, Quaternion.identity) as GameObject;
                deployedExpandex.transform.up = hit.normal;
                expandex[0] = GameObject.Find("ScaleFromOneSide").GetComponent<ExpandexController>();
                canCreateExpandex1 = false;
            }
            else if(canCreateExpandex2)
            {
                deployedExpandex2 = Instantiate(expandexPrefab2, position, Quaternion.identity) as GameObject;
                deployedExpandex2.transform.up = hit.normal;
                expandex[1] = GameObject.Find("ScaleFromOneSide2").GetComponent<ExpandexController>();
                canCreateExpandex2 = false;
            }  
        }
    }
    

    private void PickupExpandex()
    {
        if(allowedToTakeExpandex1)
        {
            Destroy(deployedExpandex);
            deployedExpandex = null;
            allowedToTakeExpandex1 = false;
            canCreateExpandex1 = true;
        }
        else if(allowedToTakeExpandex2)
        {
            Destroy(deployedExpandex2);
            deployedExpandex2 = null;
            allowedToTakeExpandex2 = false;
            canCreateExpandex2 = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.name == "ScaleFromOneSide")
            allowedToTakeExpandex1 = true;
        if (obj.name == "ScaleFromOneSide2")
            allowedToTakeExpandex2 = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.name == "ScaleFromOneSide")
            allowedToTakeExpandex1 = false;
        if (obj.name == "ScaleFromOneSide2")
            allowedToTakeExpandex2 = false;
    }
}
