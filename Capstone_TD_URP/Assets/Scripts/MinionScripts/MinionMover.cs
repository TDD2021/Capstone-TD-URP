using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MinionMover : MonoBehaviour
{
    [SerializeField]
    Transform _destination;

    public Camera cam;
    public NavMeshAgent _navMeshAgent;

    [SerializeField]
    Vector3 destination;    // added

    //public NavMeshAgent _towerNavMeshAgent;
    //public NavMeshObstacle _towerNavMeshObstacle;

    //public GameObject tower;
    public Vector3 positionOffset;

    //public GameObject EndPoint;
    //public GameObject Minion;

    //BuildManager buildManager;
    private LineRenderer lr;

    void Start()
    {
        destination = new Vector3(2.2f, 3f, 2.49f);     // added

        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        lr = this.GetComponent<LineRenderer>();

        if (_navMeshAgent == null)
            Debug.Log("nav mesh agent component is not attached to " + gameObject.name);
        else
            SetDestination();
    }

    private void SetDestination()
    {
        if (_destination != null)
        {
           // Vector3 targetVector = _destination.transform.position;       // removed
            Vector3 targetVector = destination;             //added
            _navMeshAgent.SetDestination(targetVector);
        }
    }




    // Update is called once per frame
    void Update()
    {

        if (_navMeshAgent.hasPath)
        {
            lr.positionCount = _navMeshAgent.path.corners.Length;
            lr.SetPositions(_navMeshAgent.path.corners);
            lr.enabled = true;
        }
        else 
        {
            lr.enabled = false;
            SetDestination(); // added
        }

        /*if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                //Move agent 
                //_navMeshAgent.SetDestination(hit.point);
            }

            GameObject towerToBuild = BuildManager.instance.GetBuildTower();
            //tower = (GameObject)Instantiate(towerToBuild, transform.position + positionOffset, Quaternion.Euler(Vector3.right * 0));
            tower = (GameObject)Instantiate(towerToBuild, hit.point + positionOffset, Quaternion.Euler(Vector3.right * 0));
            // tower.AddComponent(typeof(NavMeshAgent));
            tower.AddComponent(typeof(NavMeshObstacle));
            // _towerNavMeshAgent = tower.GetComponent<NavMeshAgent>();
            _towerNavMeshObstacle = tower.GetComponent<NavMeshObstacle>();
            _towerNavMeshObstacle.carving = true;
            _towerNavMeshObstacle.radius = 2;

        }*/
    }
}
