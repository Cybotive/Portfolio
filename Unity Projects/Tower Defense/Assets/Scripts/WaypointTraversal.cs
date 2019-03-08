using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointTraversal : MonoBehaviour {

    [SerializeField]
    private float speed = 5f;

    private Vector2 direction;
    //public int WPindex = 0;
    //private int WPindex = 0;
    internal int WPindex = 0;

    private float waypointX;
    private float waypointY;

    void Start ()
    {
        
    }

	void Update ()
    {

        waypointX = Waypoints.waypoints[WPindex].position.x;
        waypointY = Waypoints.waypoints[WPindex].position.y;

        direction = Vector2.zero;
        if (transform.position.y - 0.1f < waypointY)
        {
            direction += Vector2.up;
        }
        if (transform.position.x + 0.1f > waypointX)
        {
            direction += Vector2.left;
        }
        if (transform.position.y + 0.1f > waypointY)
        {
            direction += Vector2.down;
        }
        if (transform.position.x - 0.1f < waypointX)
        {
            direction += Vector2.right;
        }

        transform.Translate(direction.normalized * speed * Time.deltaTime);

        if(WPindex + 1 < Waypoints.waypoints.Length && Vector2.Distance(transform.position, Waypoints.waypoints[WPindex].position) < 0.3f)
        {
            WPindex++;
        }

    }
}
