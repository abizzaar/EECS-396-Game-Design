using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Pathchecker : MonoBehaviour {

    public bool clearpath = true;
    private NavMeshAgent pathchecker;
    private NavMeshPath path;

    // Use this for initialization
    void Start () {
        pathchecker = gameObject.GetComponent<NavMeshAgent>();
        path = pathchecker.path;
        
    }
	
	// Update is called once per frame
	void Update () {

        clearpath = pathchecker.CalculatePath(GameObject.Find("Spawner").transform.position, path);
        if (path.status != NavMeshPathStatus.PathComplete)
        {
            clearpath = false;
        }
        else
        {
            clearpath = true;
        }
    }
}
