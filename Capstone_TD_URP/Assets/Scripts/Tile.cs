using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Color hoverColor;
    public Vector3 positionOffset;
    public GameObject tower;
    private Renderer renderer;
    private Color startingColor;

    BuildManager buildManager;
    


    void Start()
    {
        renderer = GetComponent<Renderer>();
        startingColor = renderer.material.color;
        buildManager = BuildManager.instance;
    }

    void OnMouseEnter()
    {

        if (buildManager.GetBuildTower() == null)
            return;

        renderer.material.color = hoverColor;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        Debug.Log("This tile is at: " + hit.point);

    }
    void OnMouseDown()
    {
        if (buildManager.GetBuildTower() == null)
            return;


        if (tower != null)
        {
            Debug.Log("Tower Built There.");
            return;
        }

        GameObject towerToBuild = BuildManager.instance.GetBuildTower();
        tower = (GameObject)Instantiate(towerToBuild, transform.position+positionOffset, Quaternion.Euler(Vector3.right * 0));

    }
    void OnMouseExit()
    {

        renderer.material.color = startingColor;

    }

}
