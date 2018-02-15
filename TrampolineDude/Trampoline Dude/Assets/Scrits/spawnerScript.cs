using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnerScript : MonoBehaviour {

    public GameObject obsticle;
    [SerializeField]
    private GameObject hitObj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1))
        {
            //create a raycast from the camera to the plane
            Ray dir = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //if the ray hits the plane, instantiate game object
            if (Physics.Raycast(dir, out hit))
            {
                hitObj = hit.transform.gameObject;
                if ((hitObj.tag == "surface") || (hitObj.tag == "wall"))
                {
                    Quaternion objRotation = new Quaternion(hitObj.transform.rotation.x, hitObj.transform.rotation.y, hitObj.transform.rotation.z, hitObj.transform.rotation.w);
                    Object.Instantiate(obsticle, hit.point, objRotation);
                }
            }
        }
    }
}
