using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTracker : MonoBehaviour {

    private ArrayList entityList = new ArrayList();

    public static ArrayList entityListRO;

    void Start ()
    {
        entityListRO = ArrayList.ReadOnly(entityList);
        BuildEntityList();
    }
	
	void Update ()
    {
		
	}

    void FixedUpdate()
    {
        BuildEntityList();
    }

    private void BuildEntityList()
    {
        GameObject[] gOArray = GameObject.FindGameObjectsWithTag("Entity");
        //Debug.Log(entityList.Count);
        foreach (GameObject entity in gOArray)
        {
            if (!entityList.Contains(entity))
            {
                entityList.Add(entity);
            }
        }
    }
}
