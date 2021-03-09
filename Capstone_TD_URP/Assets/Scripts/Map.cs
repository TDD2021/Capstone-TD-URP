using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
	public Transform tilePrefab;
	public Transform tower;
	public Vector2 size;

	[Range(0, 1)]
	public float outlinePercent;

	void Start()
	{
		GenerateMap();
	}
	void Update() 
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			Physics.Raycast(ray, out hit);
			Debug.Log("This hit at " + hit.point);
			Transform towerPlacement = Instantiate(tower, hit.point, Quaternion.Euler(Vector3.right * 0)) as Transform;
		}

	}
	public void GenerateMap()
	{
		//name of object
		string holder = "Map Tile Array";
		//finds the child and destroys the object
		if (transform.FindChild(holder))
		{
			DestroyImmediate(transform.FindChild(holder).gameObject);
		}
		//stores the position, rotaion and scale of an object under mapHolder
		Transform mapHolder = new GameObject(holder).transform;
		mapHolder.parent = transform;

		//create a map on the xz-plane using xy coordinates that is rotated on the x-axis by 90 degrees
		for (int x = 0; x < size.x; x++)
		{
			for (int y = 0; y < size.y; y++)
			{
				Vector3 tilePosition = new Vector3(-size.x / 2 + 0.5f + x, 0, -size.y / 2 + 0.5f + y);
				Transform newTile = Instantiate(tilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
				newTile.localScale = Vector3.one * (1 - outlinePercent);
				newTile.parent = mapHolder;
			}
		}
	}
}
