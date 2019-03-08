using System;
using UnityEngine;
using System.Collections;

namespace UnityStandardAssets.CrossPlatformInput{
public class MouseLookandPlayerMovement : MonoBehaviour
{
	public static bool mouseClamped = true;

    public static readonly float movementSpeedBase = ccColliderTestGrab.movementSpeedBase;
    public static readonly float movementSpeedRunBase = ccColliderTestGrab.movementSpeedRunBase;

    public static float movementSpeed = movementSpeedBase;
    public static float movementSpeedRun = movementSpeedRunBase;
    public float finalMovementSpeed = 0;
    public float mouseSensitivity = 5;
    //public Rigidbody rb;
    public float jumpSpeed = 1;

    float verticalRotation = 0;
    public float mouseClamp = 60;

    float verticalVelocity = 0;
    public float forwardSpeed;
    float sideSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        //rotation
		#if !MOBILE_INPUT
        float rotY = Input.GetAxis("Mouse X") * mouseSensitivity;
		verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
		#else
		float rotY = CrossPlatformInputManager.GetAxis("Mouse X") * mouseSensitivity;
		verticalRotation -= CrossPlatformInputManager.GetAxis("Mouse Y") * mouseSensitivity;
		#endif
		
		if (mouseClamped)
		transform.Rotate(0, rotY, 0);
		
        verticalRotation = Mathf.Clamp(verticalRotation, -mouseClamp, mouseClamp);

		if (mouseClamped)
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        CharacterController cc = GetComponent<CharacterController>();

        if (Input.GetButton("Run"))
        {
            finalMovementSpeed = ccColliderTestGrab.movementSpeedRun;
        }
        else
        {
            finalMovementSpeed = ccColliderTestGrab.movementSpeed;
        }

        //Debug.Log(ButtonScript.bkButtonPressed);
        //movement
        if (cc.isGrounded)
        {
			if (UpButtonScript.fwdButtonPressed) {
                Debug.Log("Going Forward");
				forwardSpeed = finalMovementSpeed;
			}
			if (DownButtonScript.bkButtonPressed) {
                Debug.Log("Going Backward");
                forwardSpeed = -finalMovementSpeed;
			}
                if (!UpButtonScript.fwdButtonPressed && !DownButtonScript.bkButtonPressed) {
                    forwardSpeed = 0;
                }
            if (LeftButtonScript.lftButtonPressed)
            {
                Debug.Log("Going Left");
                sideSpeed = -finalMovementSpeed;
            }
            if (RightButtonScript.rgtButtonPressed)
            {
                Debug.Log("Going Right");
                sideSpeed = finalMovementSpeed;
            }
                if(!LeftButtonScript.lftButtonPressed && !RightButtonScript.rgtButtonPressed)
                {
                    sideSpeed = 0;
                }
                if (!UpButtonScript.fwdButtonPressed && !DownButtonScript.bkButtonPressed && !LeftButtonScript.lftButtonPressed && !RightButtonScript.rgtButtonPressed) {
				    forwardSpeed = Input.GetAxis("Vertical") * finalMovementSpeed;
				    sideSpeed = Input.GetAxis ("Horizontal") * finalMovementSpeed;
				    //forwardSpeed = CrossPlatformInputManager.GetAxis("Vertical") * finalMovementSpeed;
				    //sideSpeed = CrossPlatformInputManager.GetAxis ("Horizontal") * finalMovementSpeed;
			    }
        }

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (cc.isGrounded)
        {
				if (CrossPlatformInputManager.GetButton("Jump") || Input.GetButton("Jump") || JumpButtonScript.jmpButtonPressed) {
				verticalVelocity = jumpSpeed;
				}
        }

        Vector3 velocity = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);
        velocity = transform.rotation * velocity;

        cc.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Quit"))
            Application.Quit();

		if (Input.GetMouseButtonDown (2))
			mouseClamped = !mouseClamped;

		if (mouseClamped == true) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else if (mouseClamped == false) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
    }
}
}