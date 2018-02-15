using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

    //Vector3 direction;
    absorberScript absorber;
    [SerializeField]float speed;

	// Use this for initialization
	void Start () {
        //direction = transform.forward;
        GameObject p = GameObject.Find("spawner");
        absorber = p.GetComponent<absorberScript>();
	}
	
	// Update is called once per frame
	void Update () {
        //move in direction
        gameObject.transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Debug.DrawRay(transform.position, transform.forward);

	}

    void OnCollisionEnter(Collision collision)
    {
        print("hit detected");
        GameObject other = collision.transform.gameObject;
        if (other.tag == "surface")
        {
            print("hit surface");
            Quaternion objRotation = other.transform.rotation;
            Object.Instantiate(absorber.getObsticle(), gameObject.transform.position, other.transform.rotation);
        }
    }
}
