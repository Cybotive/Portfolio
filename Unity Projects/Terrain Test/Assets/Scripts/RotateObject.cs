using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {

    public int speed = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //transform.Rotate(0, 5, 0);
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
