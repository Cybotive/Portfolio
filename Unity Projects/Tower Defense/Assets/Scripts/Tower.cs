using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    
    [SerializeField]
    private float range = 5f; //distance the tower will be able to see the gameObject in grid units
    [SerializeField]
#pragma warning disable CS0649 // Field 'Tower.spriteRotation' is never assigned to, and will always have its default value 0
    private float spriteRotation;
#pragma warning restore CS0649 // Field 'Tower.spriteRotation' is never assigned to, and will always have its default value 0

    [SerializeField]
#pragma warning disable CS0649 // Field 'Tower.towerRotation' is never assigned to, and will always have its default value 0
    private float towerRotation;
#pragma warning restore CS0649 // Field 'Tower.towerRotation' is never assigned to, and will always have its default value 0

    private bool obstructed = true;

    private GameObject entity;

    void Start ()
    {
        
	}
	
	void Update ()
    {
        /*if (entity == null || obstructed == true || FindDistanceTo(entity) > range)
        {
            GetBestEntity();
        }*/

        if (entity == null)
            return;

        if (Vector2.Distance(transform.position, entity.transform.position) <= range && obstructed == false)
        {
            transform.SetPositionAndRotation(transform.position, FaceObject(transform.position, entity.transform.position, spriteRotation));
        }
        else if (Vector2.Distance(transform.position, entity.transform.position) > range || obstructed == true)
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.AngleAxis(spriteRotation + towerRotation, Vector3.forward));
        }

    }

    void FixedUpdate()
    {
        if (entity == null || obstructed == true || FindDistanceTo(entity) > range)
        {
            GetBestEntity();
        }

        if (entity != null)
        {
            RaycastHit2D hit = RaycastTo(transform.position, entity); //set a Physics2D.Raycast to see if we hit the player, or if we hit a wall, before firing
            Debug.DrawRay(transform.position, (entity.transform.position - transform.position), Color.red, 0.05f);
            obstructed = IsObstructed(hit, "Entities");
        }
    }

    private RaycastHit2D RaycastTo(Vector2 position, GameObject entityTemp)
    {
        RaycastHit2D hit = Physics2D.Raycast(position, FindDirTo(position, entityTemp.transform.position));
        return hit;
    }

    private bool IsObstructed(RaycastHit2D hit, string layerName)
    {
        if (hit.collider != null && hit.transform.gameObject.layer.Equals(LayerMask.NameToLayer(layerName)))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void GetBestEntity()
    {
        float minDistance = range;
        //int minWP = Waypoints.waypoints.Length - 1; //minus 1 is there to make first waypoint address '0'
        int maxWP = 0;
        float minDistToWP = Mathf.Infinity;
        GameObject entityTemp = null;

        foreach (GameObject entityObj in EntityTracker.entityListRO)
	    {
            float tempDistance = Vector2.Distance(transform.position, entityObj.transform.position);
            
            RaycastHit2D hit = RaycastTo(transform.position, entityObj);
            bool obstructedLocal = IsObstructed(hit, "Entities");

            if (tempDistance <= range && entityObj.GetComponent<WaypointTraversal>().WPindex >= maxWP && FindDistanceBetween(entityObj, Waypoints.waypoints[maxWP].gameObject) <= minDistToWP && obstructedLocal == false) //need to restrict maxWP test to only entities in range NVM, already is
            {
                maxWP = entityObj.GetComponent<WaypointTraversal>().WPindex;
                minDistToWP = FindDistanceBetween(entityObj, Waypoints.waypoints[maxWP].gameObject);
                entityTemp = entityObj;
            }
        }
        entity = entityTemp;

        if(entity != null)
        {
            int WPindex = entity.GetComponent<WaypointTraversal>().WPindex;
            Debug.Log("Going to WP: " + WPindex);
        }
    }

    private float FindDistanceTo(GameObject gameObj)
    {
        return Vector2.Distance(transform.position, gameObj.transform.position);
    }

    private float FindDistanceBetween(GameObject gameObjA, GameObject gameObjB)
    {
        return Vector2.Distance(gameObjA.transform.position, gameObjB.transform.position);
    }

    private Vector2 FindDirTo(Vector2 startingPosition, Vector2 targetPosition)
    {
        return (targetPosition - startingPosition);
    }

    private Quaternion FaceObject(Vector2 startingPosition, Vector2 targetPosition, float offsetAngle)
    {
        Vector2 direction = targetPosition - startingPosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= offsetAngle;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
 