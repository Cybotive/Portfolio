using UnityEngine;
using System.Collections;

public class ccColliderTest : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        
	}

    string GetMouseHoverObject()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            return hit.collider.tag;
        }
        return null;
    }

    void Update ()
    {
        Debug.Log(GetMouseHoverObject());
    }
}
