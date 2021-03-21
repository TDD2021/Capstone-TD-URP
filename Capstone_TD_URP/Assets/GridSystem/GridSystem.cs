using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private Transform testTransform;

    int gridWidth;
    int gridHeight;

    private Grid3D<GridObject> grid;
    private Ray ray;
    [SerializeField] private LayerMask mouseColliderLayerMask;
    BuildManager buildManager;
 


    public class GridObject
    {
        private Grid3D<GridObject> grid;
        private int x;
        private int z;
        private Transform transform;

        public GridObject(Grid3D<GridObject> grid, int x, int z)
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

        public bool CanBuild()
        {
            return transform == null;
        }

        public override string ToString()
        {
            return x + ", " + z + "\n" + transform;
        }
    }

    private void Awake()
    {
        gridWidth = 10;
        gridHeight = 10;
        float cellSize = 5f;

        grid = new Grid3D<GridObject>(gridWidth, gridHeight, cellSize, Vector3.zero, (Grid3D<GridObject> g, int x, int z) => new GridObject(g, x, z));
        buildManager = BuildManager.instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    grid.GetXZ(Utility.GetMouseWorldPosition(mouseColliderLayerMask), out int x, out int z);

        //    if (x >= 0 && z >= 0 && x < gridWidth && z < gridHeight)
        //    {
        //        GridObject gridObject = grid.GetGridObject(x, z);
        //        if (gridObject.CanBuild())
        //        {
        //            Transform builtTransform = Instantiate(testTransform, grid.GetWorldPosition(x, z), Quaternion.identity);
        //            gridObject.SetTransform(builtTransform);
        //        }
        //    }
        //}
        if (Input.GetMouseButtonDown(0))
        {
            //logic for selling tower using tags and raycasting
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 999f, mouseColliderLayerMask)) {
                Debug.Log(hit.collider.gameObject.tag);
                Debug.Log(BuildManager.instance.GetSellTower());
                if (hit.collider.gameObject.tag == "Tower" && BuildManager.instance.GetSellTower()) {
                    Destroy(hit.transform.gameObject);
                    BuildManager.instance.SetSellTower(false);
                    Debug.Log(BuildManager.instance.GetSellTower());
                }
                else 
                {
                    Debug.Log("Did not sell");
                }
            }

            if (buildManager.GetBuildTower() == null)
                return;

            grid.GetXZ(Utility.GetMouseWorldPosition(mouseColliderLayerMask), out int x, out int z);

            if (x >= 0 && z >= 0 && x < gridWidth && z < gridHeight)
            {
                GridObject gridObject = grid.GetGridObject(x, z);
                Debug.Log("Can Build: "+ gridObject.CanBuild());
                if (gridObject.CanBuild())
                {
                    GameObject towerToBuild = BuildManager.instance.GetBuildTower();
                    Transform tower = Instantiate(towerToBuild.transform, grid.GetWorldPosition(x, z), Quaternion.identity);
                    gridObject.SetTransform(tower);
                    //Remove current selection
                    BuildManager.instance.SetBuildTower(null);

                }
          
                
                 
                
            }
        }
    }
   

}
