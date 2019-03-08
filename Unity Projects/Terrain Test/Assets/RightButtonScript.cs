using UnityEngine;
using UnityEngine.UI;

public class RightButtonScript : Button
{

    public static bool rgtButtonPressed = false;

    // Use this for initialization
    new void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsPressed())
        {
            rgtButtonPressed = true;
        }
        else
        {
            rgtButtonPressed = false;
        }
    }
}
