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
    //public NavMeshAgent _towerNavMeshAgent;
    //public NavMeshObstacle _towerNavMeshObstacle;

    //public GameObject tower;
    public Vector3 positionOffset;

    //public GameObject EndPoint;
    //public GameObject Minion;

    //BuildManager buildManager;

    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
            Debug.Log("nav mesh agent component is not attached to " + gameObject.name);
        else
            SetDestination();
    }

    private void SetDestination()
    {
        if (_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
           
        }
    }

    // Update is called once per frame
    void Update()
   { 
            if (_navMeshAgent.nextPosition == _navMeshAgent.destination)
            {
                Destroy(_navMeshAgent);
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
