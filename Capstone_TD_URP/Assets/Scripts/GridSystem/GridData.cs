using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    private Grid3D<GridData> grid;
    private int x;
    private int z;
    private Transform transform;

    public GridData(Grid3D<GridData> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
    }

    public void SetTransform(Transform transform)
    {
        this.transform = transform;
        grid.TriggerGridObjectChanged(x, z);
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void ClearTransform()
    {
        transform = null;
    }

    public bool CanBuild(Transform buildChecker)
    {
        if (buildChecker.GetComponent<BuildCheckerScript>().CanBuild && transform == null)
        {
            Debug.Log("Can build");
            return true;
        }
        else
        {
            Debug.Log("No build");
            return false;
        }

        //return transform == null;
    }

    public override string ToString()
    {
        return x + ", " + z + "\n" + transform;
    }
}
