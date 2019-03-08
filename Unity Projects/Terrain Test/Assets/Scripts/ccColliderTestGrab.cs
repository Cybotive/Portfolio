using UnityEngine;
using System.Collections;

public class ccColliderTestGrab : MonoBehaviour
{
    public int range;
    public static float boulderCount;
    public AudioClip rockPickUp;
    public AudioSource rockPick;
    public float rockMultiSpeed;
    public float movingSpeed;

	public static readonly float movementSpeedBase = 4;
	public static readonly float movementSpeedRunBase = 8;

	public static float movementSpeed = movementSpeedBase;
	public static float movementSpeedRun = movementSpeedRunBase;

    // Use this for initialization
    void Start ()
    {
        
	}

    string GetMouseHoverObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, range))
        {
            //return hit.collider.tag;
            if (Input.GetButton("Grab"))
            {
                GameObject objectThing = hit.collider.gameObject;
                if (hit.collider.gameObject.tag == "Rock"){
                    AudioSource rockPick = GetComponent<AudioSource>();
                    rockPick.Play();
                    //finalMovementSpeed *= (1-(boulderCount/10));
                    //Debug.Log(finalMovementSpeed);
                    boulderCount += 1;

                    Destroy(objectThing);
                }
            }
        }
        return null;
    }

    void Update ()
    {
        GetMouseHoverObject();
        if (boulderCount > 0)
        {
            rockMultiSpeed = ((boulderCount / 3) + 0.7f) * 1.05f;
            movementSpeed = movementSpeedBase / rockMultiSpeed;
            movementSpeedRun = movementSpeedRunBase / rockMultiSpeed;
            movingSpeed = movementSpeed;
        }
        else
        {
            rockMultiSpeed = 1;
            movementSpeed = movementSpeedBase / rockMultiSpeed;
            movementSpeedRun = movementSpeedRunBase / rockMultiSpeed;
            movingSpeed = movementSpeed;
        }
        /*if (Input.GetKey("g"))
        {
            string hi = hit.collider.gameObject.name;
            Debug.Log(hi);
        }*/
    }
}
