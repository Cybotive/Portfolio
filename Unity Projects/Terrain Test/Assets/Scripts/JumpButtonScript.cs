using UnityEngine;
using UnityEngine.UI;

public class JumpButtonScript : Button
{

    public static bool jmpButtonPressed = false;

    // Use this for initialization
    new void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsPressed())
        {
            jmpButtonPressed = true;
        }
        else
        {
            jmpButtonPressed = false;
        }
    }
}
