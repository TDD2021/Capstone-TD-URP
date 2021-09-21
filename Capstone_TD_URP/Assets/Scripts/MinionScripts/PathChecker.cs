using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathChecker : MonoBehaviour
{
    public NavMeshSurface surface;
    public Transform destination;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckPath()
    {
        //NavMeshHit hit;
        NavMeshPath path = new NavMeshPath();
        NavMeshQueryFilter nq = new NavMeshQueryFilter();
        /*if (!NavMesh.SamplePosition(destination.position, out hit, 500, NavMesh.GetAreaFromName("Walkable")))
        {
            Debug.LogWarning("Target not on navmesh (NavMesh)");
        }*/
        nq.agentTypeID = surface.agentTypeID;
        nq.areaMask = 1 << NavMesh.GetAreaFromName("Walkable");
        //_ = gameObject.GetComponent<NavMeshAgent>().CalculatePath(new Vector3(2.2f, 3f, 2.49f), path);
        _ = NavMesh.CalculatePath(gameObject.transform.position, destination.position, nq, path);
        Debug.Log("Path check result: " + path.status.ToString());
        return path.status == NavMeshPathStatus.PathComplete;
    }
}
