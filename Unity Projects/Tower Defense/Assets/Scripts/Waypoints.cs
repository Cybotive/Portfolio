using UnityEngine;

public class Waypoints : MonoBehaviour {

    public static Transform[] waypoints;

    void Awake()
    {
        waypoints = new Transform[transform.childCount]; //eventually might want to turn this into a key-pair list, where key is the address starting from 0, and pair is the waypoint Transform
        for (int a = 0; a < waypoints.Length; a++)
        {
            waypoints[a] = transform.GetChild(a);
        }
    }
}
