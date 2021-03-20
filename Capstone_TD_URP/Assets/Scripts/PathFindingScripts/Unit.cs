using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
	public Transform target;
	float speed = 10;
	Vector3[] path;
	int targetIndex;

	//Added..
	public Vector3 positionOffset;
	public GameObject tower;
	BuildManager buildManager;
	public Camera cam;
	public NavMeshAgent _navMeshAgent;
	public NavMeshAgent _towerNavMeshAgent;
	public NavMeshObstacle _towerNavMeshObstacle;

	void Start()
	{
		PathManager.RequestPath(transform.position, target.position, OnPathFound);
		
		buildManager = BuildManager.instance;
		buildManager.SetBuildTower(buildManager.tower1);
		positionOffset = new Vector3(0, 1.09f, 0);
	}

    private void Update()
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

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
	{


		if (pathSuccessful)
		{
			path = newPath;
			targetIndex = 0;
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator FollowPath()
	{
		Vector3 currentWaypoint = path[0];
		while (true)
		{
			if (transform.position == currentWaypoint)
			{
				targetIndex++;
				if (targetIndex >= path.Length)
				{
					yield break;
				}
				currentWaypoint = path[targetIndex];
			}

			transform.position = Vector3.MoveTowards(transform.position , currentWaypoint + positionOffset, speed * Time.deltaTime);
			Debug.Log("Waypoint X: " + currentWaypoint.x + "    Waypoint Y: " + currentWaypoint.y + "        Waypoint Z: " + currentWaypoint.z);
			yield return null;

		}
	}

	public void OnDrawGizmos()
	{
		if (path != null)
		{
			for (int i = targetIndex; i < path.Length; i++)
			{
				Gizmos.color = Color.black;
				Gizmos.DrawCube(path[i], Vector3.one);

				if (i == targetIndex)
				{
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else
				{
					Gizmos.DrawLine(path[i - 1], path[i]);
				}
			}
		}
	}
}
