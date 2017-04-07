using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandexRemote : MonoBehaviour
{

    [SerializeField]
    private GameObject expandexPrefab;
    private GameObject deployedExpandex;
    private ExpandexController expandex;
    private bool allowedToTakeExpandex;
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
            if (!deployedExpandex)
                StickExpandex();
            else if (deployedExpandex && allowedToTakeExpandex)
                PickupExpandex();
        }
        else if (Input.GetKeyDown(KeyCode.E))
            ToggleExpandexActivation();

    }


    private void ToggleExpandexActivation()
    {
        if (deployedExpandex)
            expandex.ToggleActivation();
    }
    

    private void StickExpandex()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistanceToStick, stickableSurface))
        {
            Vector3 position = hit.point;

            deployedExpandex = Instantiate(expandexPrefab, position, Quaternion.identity) as GameObject;
            deployedExpandex.transform.up = hit.normal;
            expandex = GameObject.Find("ScaleFromOneSide").GetComponent<ExpandexController>();
        }
    }
    

    private void PickupExpandex()
    {
        Destroy(deployedExpandex);
        deployedExpandex = null;
        allowedToTakeExpandex = false;
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
