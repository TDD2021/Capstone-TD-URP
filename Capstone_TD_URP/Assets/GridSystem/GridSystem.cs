using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private List<TowerData> towerDataList;
    private TowerData selectedTowerData;

    int gridWidth;
    int gridHeight;

    private Grid3D<GridObject> grid;
    private Ray ray;
    [SerializeField] private LayerMask mouseColliderLayerMask;

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

        selectedTowerData = towerDataList[0];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            grid.GetXZ(Utility.GetMouseWorldPosition(mouseColliderLayerMask), out int x, out int z);

            if (x >= 0 && z >= 0 && x < gridWidth && z < gridHeight)
            {
                GridObject gridObject = grid.GetGridObject(x, z);
                if (gridObject.CanBuild())
                {
                    Transform builtTransform = Instantiate(selectedTowerData.Prefab.transform, grid.GetWorldPosition(x, z), Quaternion.identity);
                    gridObject.SetTransform(builtTransform);
                }
            }
        }
    }
}
