using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    Transform _destination;

    public Camera cam;
    public NavMeshAgent _navMeshAgent;
    public NavMeshAgent _towerNavMeshAgent;
    public NavMeshObstacle _towerNavMeshObstacle;

    public GameObject tower;
    public Vector3 positionOffset;

    BuildManager buildManager;

    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();

        if (_navMeshAgent == null)
            Debug.Log("nav mesh agent component is not attached to " + gameObject.name);
        else
            SetDestination();

       /* buildManager = BuildManager.instance;

        buildManager.SetBuildTower(buildManager.tower1);
        positionOffset = new Vector3(0, 0.5f, 0);*/
    }

    private void SetDestination()
    {
        if(_destination != null)
        {
            Vector3 targetVector = _destination.transform.position;
            _navMeshAgent.SetDestination(targetVector);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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

        }
    }
}
