using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float movementSpeed = 5.0f;
    public float mouseSens = 5.0f;
    public float jumpSpeed = 10.0f;
    public float bounceSpeed = 20.0f;
    public float speedLimit = 5.1f;

    public float forwardSpeed = 0.0f;
    public float sideSpeed = 0.0f;
    public bool bounce = false;
    Vector3 speed;

    float verticalRotation = 0;
    public float upDownRange = 60.0f;

    float verticalVelocity = 0;

    CharacterController characterController;
    [SerializeField]
    private GameObject lastCollision;

    
    // Use this for initialization
    void Start()
    {
        Screen.lockCursor = true;
        //CharacterController characterController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {

		float joyX = Mathf.Abs (Input.GetAxis ("Joystick X"));
		float joyY = Mathf.Abs (Input.GetAxis ("Joystick Y"));

        CharacterController characterController = GetComponent<CharacterController>();
        //Rotation

        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSens;
        transform.Rotate(0, rotLeftRight, 0);

		if (joyX>.05){
			transform.Rotate(0, Input.GetAxis("Joystick X") * 3f, 0);
		}

		if (joyY>.05){
			verticalRotation += Input.GetAxis("Joystick Y") * 3f;
		}

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSens;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        //movement

        forwardSpeed += Input.GetAxis("Vertical") * movementSpeed;
        sideSpeed += Input.GetAxis("Horizontal") * movementSpeed;

        forwardSpeed *= .8f;
        sideSpeed *= .8f;

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (characterController.isGrounded && Input.GetButtonDown("Jump") && !bounce)
        {
            verticalVelocity = jumpSpeed;
        }
        else if (bounce)
        {
            Quaternion bounceAngle = lastCollision.transform.rotation;
            //Vector3 bounceVector = new Vector3(bounceAngle.eulerAngles.x, bounceAngle.eulerAngles.y, bounceAngle.eulerAngles.z);
            Vector3 bbounceVector = lastCollision.transform.up * bounceSpeed;
            bbounceVector = bounceAngle * bbounceVector;
            print(bbounceVector);
            verticalVelocity = bbounceVector.y;
            sideSpeed = bbounceVector.x;
            forwardSpeed = bbounceVector.z;
        }

        bounce = false;

        speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);

        speed = transform.rotation * speed;
        if (!characterController.isGrounded)
        {
            print(speed);
        }

        characterController.Move(speed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other) {

        lastCollision = other.gameObject;
        if (lastCollision.tag == "trampoline")
        {
            bounce = true;
        }
    }
}
