using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class absorberScript : MonoBehaviour {

    [SerializeField]private GameObject bullet;
    private GameObject obsticle;
    private GameObject hitObj;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 100f, Color.cyan);
        if (Input.GetMouseButtonDown(0))
        {
            //create a raycast from the camera to the plane
            Ray dir = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            //;

            //if the ray hits the plane, instantiate game object
            if (Physics.Raycast(dir, out hit))
            {
                //if object is obtainable get object
                if (hit.transform.gameObject.tag == "trampoline")
                {
                    InteractableScript script = hit.transform.gameObject.GetComponent<InteractableScript>();
                    obsticle = script.prefab;
                    Destroy(hit.transform.gameObject);
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {

            Object.Instantiate(bullet, Camera.main.transform.position, Camera.main.transform.rotation);
            //create a raycast from the camera to the plane
            /*
            Ray dir = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            RaycastHit hit;

            //if the ray hits the plane, instantiate game object
            if (Physics.Raycast(dir, out hit))
            {
                hitObj = hit.transform.gameObject;
                if (hitObj.tag == "surface")
                {
                    Quaternion objRotation = new Quaternion(hitObj.transform.rotation.x, hitObj.transform.rotation.y, hitObj.transform.rotation.z, hitObj.transform.rotation.w);
                    Object.Instantiate(obsticle, hit.point, hitObj.transform.rotation);
                }
            }
            */
        }
    }

    public GameObject getObsticle()
    {
        return obsticle;
    }
}
