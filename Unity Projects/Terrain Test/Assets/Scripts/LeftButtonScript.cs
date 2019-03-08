using UnityEngine;
using UnityEngine.UI;

public class LeftButtonScript : Button
{

    public static bool lftButtonPressed = false;

    // Use this for initialization
    new void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsPressed())
        {
            lftButtonPressed = true;
        }
        else
        {
            lftButtonPressed = false;
        }
    }
}
