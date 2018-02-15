using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller1 : MonoBehaviour {

    public float movementSpeed = 5.0f;
    public float mouseSens = 5.0f;
    public float jumpSpeed = 10.0f;
    public float bounceSpeed = 20.0f;
    public float speedLimit = 5.1f;
    Vector3 move;


    bool bounce = false;

    float verticalRotation = 0;
    public float upDownRange = 60.0f;

    float verticalVelocity = 0;

    //CharacterController characterController;
    Rigidbody rb;
    [SerializeField] private GameObject lastCollision;
    [SerializeField] private float height;

    // Use this for initialization
    void Start()
    {
        Screen.lockCursor = true;
        rb = GetComponent<Rigidbody>();
        //CharacterController characterController = GetComponent<CharacterController>();
    }
	
	// Update is called once per frame
	void Update () {

		float joyX = Mathf.Abs (Input.GetAxis ("Joystick X"));
		float joyY = Mathf.Abs (Input.GetAxis ("Joystick Y"));

        //CharacterController characterController = GetComponent<CharacterController>();
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

        move.z = Input.GetAxis("Vertical");
        move.x = Input.GetAxis("Horizontal");

        if(move != Vector3.zero)
        {
            move = Camera.main.transform.rotation * move;
            move.y = 0f;
            move = move.normalized;
        }

        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, height);
        Debug.DrawLine(transform.position, transform.position + Vector3.down * height, Color.cyan);

        if (isGrounded)
        {
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.VelocityChange);
        }

        //check if grounded
        //if grounded move normally
        //else in air
        //if in air move using momentum
        //
        /*
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
        */
	}

    private void FixedUpdate()
    {
        //move character
        rb.MovePosition(rb.position + move * movementSpeed * Time.fixedDeltaTime);
    }

    void OnTriggerEnter(Collider other) {
        //probably will put this on the trampoline object so it will be able to push anything with a rigidbody rather than just the player
        lastCollision = other.gameObject;
        if (lastCollision.tag == "trampoline")
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(lastCollision.transform.up * bounceSpeed, ForceMode.Impulse);
        }
    }
}
