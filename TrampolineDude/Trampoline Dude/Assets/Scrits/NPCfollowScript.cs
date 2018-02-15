using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCfollowScript : MonoBehaviour {

	public GameObject player;
	public float moveSpeed;
	public float turnSpeed;
	public float satisfactionRadius;

	private Rigidbody rb;
	private bool bInRange;
	private Vector3 direction;
	private Quaternion lookRotation;


	// Use this for initialization
	void Start () {
		rb = this.gameObject.GetComponent<Rigidbody> ();
		rb.constraints = RigidbodyConstraints.FreezeRotationX;//only because cube
		rb.constraints = RigidbodyConstraints.FreezeRotationZ;
	}
	
	// Update is called once per frame
	void Update () {
		if (bInRange) {
			//Get direction and movement
			direction = player.transform.position - this.transform.position;
			direction = new Vector3 (direction.x, direction.y + 1f, direction.z);
			lookRotation = Quaternion.LookRotation (direction);
		}

		if (bInRange && direction.magnitude > satisfactionRadius) {
		//Rotate and move
			transform.rotation = Quaternion.Lerp(transform.rotation , lookRotation, turnSpeed * Time.deltaTime);
			rb.velocity = direction * moveSpeed;
		} else if (bInRange == false) {
			//Stop
			rb.velocity = Vector3.zero;
		}
	}
	//Player is in range
	void OnTriggerEnter (Collider other) {
		if (other.transform.gameObject.tag == "Player") {
			bInRange = true;
		}
	}
	//Player is out of range
	void OnTriggerExit (Collider other) {
		if (other.transform.gameObject.tag == "Player") {
			bInRange = false;
		}
	}

}
