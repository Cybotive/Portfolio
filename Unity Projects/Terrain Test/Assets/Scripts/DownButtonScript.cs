using UnityEngine;
using UnityEngine.UI;

public class DownButtonScript : Button {

    public static bool bkButtonPressed = false;

    // Use this for initialization
    new void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (IsPressed())
        {
            bkButtonPressed = true;
        }
        else
        {
            bkButtonPressed = false;
        }
    }
}
