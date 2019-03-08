using UnityEngine;
using UnityEngine.UI;

public class UpButtonScript : Button {

	//public AudioClip buttonSFXClip;
	public AudioSource buttonSFX;
    public static bool fwdButtonPressed = false;

    // Use this for initialization
    new void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (IsPressed())
        {
            fwdButtonPressed = true;
        }
        else
        {
            fwdButtonPressed = false;
        }
	}

		
}
