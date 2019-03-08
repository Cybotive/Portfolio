using UnityEngine;
using System.Collections;

public class throwRock : MonoBehaviour {

    public GameObject toThrow;
    GameObject theRock;
    bool throwDown;
    float distance = 3;
    public GameObject cameraView;
    public Camera cameraThing;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//remmed line below is for debugging purposes only
		//ccColliderTestGrab.boulderCount = 2;
        if (Input.GetButtonDown("Throw") && throwDown == false && ccColliderTestGrab.boulderCount >= 1)
        {
            throwDown = true;
            //Vector3 cameraPos = GetComponent<Camera>().transform.localPosition + new Vector3(0, 1, 2);
            Vector3 inFrontOfPlayer = transform.position + transform.forward * distance + transform.up;
            Vector3 inFrontOfView = cameraView.transform.position;
            theRock = Instantiate(toThrow, inFrontOfView, cameraView.transform.rotation) as GameObject;
			//added inFrontOfView
			//theRock.GetComponent<Rigidbody>().AddForce(transform.forward * 2000, ForceMode.Impulse);
			theRock.GetComponent<Rigidbody>().AddForce(cameraView.transform.forward * 2000, ForceMode.Impulse);
            Debug.Log(theRock.gameObject.name);
            ccColliderTestGrab.boulderCount -= 1;
        }
        else
        {
            throwDown = false;
        }
    }
}
