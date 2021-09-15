using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridSystem : MonoBehaviour
{
    public GameData gameData;

    public Transform gridPlane;

    [SerializeField] private Transform testTransform;

    [SerializeField] private List<TowerData> towerDataList;
    private TowerData towerData;

    [SerializeField] private List<PlayerData>PlayersDataList;
    private PlayerData playerData;

    int gridWidth;
    int gridHeight;

    private Grid3D<GridData> grid;
    private Ray ray;
    [SerializeField] private LayerMask mouseColliderLayerMask;
    BuildManager buildManager;

    private GridData selectedSpace;
    private Transform buildChecker;
 
    public GameObject TowerPanel;
    private bool ShowTowerMenu = false;

    private int buildX, buildZ;


    /*public class GridObject
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
    }*/

    private void Awake()
    {
        gridWidth = gameData.GridWidth;
        gridHeight = gameData.GridHeight;
        float cellSize = gameData.CellSize;

        

        grid = new Grid3D<GridData>(gridWidth, gridHeight, cellSize, Vector3.zero, (Grid3D<GridData> g, int x, int z) => new GridData(g, x, z));

        towerData = towerDataList[0];

        buildManager = BuildManager.instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        buildManager = BuildManager.instance;

        buildChecker = Instantiate(gameData.BuildChecker, grid.GetWorldPosition(0, 0), Quaternion.identity);
        //buildChecker.gameObject.SetActive(false);

        

        //TowerPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        SetGridPlane();

        /*int checkX;
        int checkY;
        grid.GetXZ(Utility.GetMouseWorldPosition(mouseColliderLayerMask), out checkX, out checkY);*/
        //buildChecker.position = grid.GetWorldPosition(checkX, checkY);

        if (Input.GetMouseButtonDown(0))
        {
            if (!TowerPanel.activeSelf)
            {
                int checkX;
                int checkY;
                grid.GetXZ(Utility.GetMouseWorldPosition(mouseColliderLayerMask), out checkX, out checkY);

                buildChecker.position = grid.GetWorldPosition(checkX, checkY);

                grid.GetXZ(Utility.GetMouseWorldPosition(mouseColliderLayerMask), out int x, out int z);

                if (x >= 0 && z >= 0 && x < gridWidth && z < gridHeight)
                {
                    buildX = x;
                    buildZ = z;

                    selectedSpace = grid.GetGridObject(x, z);

                    if (selectedSpace.CanBuild())
                    {
                        // Show Buy Menu - currently same menu (TODO: Add separate contextual menus!)
                        TowerPanel.SetActive(true);
                        for (int i = 0; i < TowerPanel.transform.childCount; i++)
                        {
                            TowerPanel.transform.GetChild(i).GetComponent<Button>().interactable = false;
                        }

                        TowerPanel.transform.GetChild(0).GetComponent<Button>().interactable = true;
                    }
                    else if (selectedSpace.GetTransform() != null)
                    {
                        // Show Sell/Modify Menu - currently same menu (TODO: Add separate contextual menus!)
                        TowerPanel.SetActive(true);
                        for (int i = 0; i < TowerPanel.transform.childCount; i++)
                        {
                            TowerPanel.transform.GetChild(i).GetComponent<Button>().interactable = false;
                        }

                        TowerPanel.transform.GetChild(2).GetComponent<Button>().interactable = true;
                    }
                }
            }
        }
        /*if (Input.GetMouseButtonDown(0))
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

         

            //grid.GetXZ(Utility.GetMouseWorldPosition(mouseColliderLayerMask), out int x, out int z);

            //if (x >= 0 && z >= 0 && x < gridWidth && z < gridHeight)
            //{
            //    GridObject gridObject = grid.GetGridObject(x, z);
            //    Debug.Log("Can Build: " + gridObject.CanBuild());
            //    Debug.Log("BuildManager status" + buildManager.GetBuildTower() != null);
            //    if (gridObject.CanBuild() && buildManager.GetBuildTower() != null)
            //    {
            //        GameObject towerToBuild = BuildManager.instance.GetBuildTower();
            //        Transform tower = Instantiate(towerToBuild.transform, grid.GetWorldPosition(x, z), Quaternion.identity);
            //        gridObject.SetTransform(tower);
            //        //Remove current selection
            //        BuildManager.instance.SetBuildTower(null);

                }
          
                
                 
                
            }
        }*/
    }

    public void BuyTower(int towerId)
    {
        /*int checkX;
        int checkY;
        grid.GetXZ(Utility.GetMouseWorldPosition(mouseColliderLayerMask), out checkX, out checkY);

        buildChecker.position = grid.GetWorldPosition(checkX, checkY);*/
       
        Debug.Log("Buy clicked: " + towerId);
        if (towerId < towerDataList.Count && !selectedSpace.IsObstructed(buildChecker.GetChild(0).transform))
        {
            Debug.Log("(Placing) Tower id: " + towerId);
            towerData = towerDataList[towerId];
            playerData = PlayersDataList[0];
            if (playerData.Currency >= towerData.Cost)
            {
                Transform builtTransform = Instantiate(towerData.Prefab, grid.GetWorldPosition(buildX, buildZ), Quaternion.identity);
                selectedSpace.SetTransform(builtTransform);
                int newCost = playerData.Currency - towerData.Cost;
                playerData.setCurrency(newCost);
            }
            Debug.Log("(Player 1 Bank: " + playerData.Currency);

        }

        TowerPanel.SetActive(false);

        buildChecker.position = grid.GetWorldPosition(-5, -5);
    }

    public void SellTower()
    {
        Debug.Log("sell clicked");
        TowerPanel.SetActive(false);
    }

    private void SetGridPlane()
    {
        gridPlane.localScale = new Vector3((gameData.CellSize * gameData.GridWidth) / 10, 1, (gameData.CellSize * gameData.GridHeight) / 10);
        gridPlane.position = this.transform.position + new Vector3(0, 0.2f, 0);
        //gridPlane.GetChild(0).SetVector("GridTiling", new Vector4(gameData.GridWidth, gameData.GridHeight, 0, 0));
        gridPlane.GetChild(0).gameObject.GetComponent<Renderer>().material.SetVector("GridTiling", new Vector4(gameData.GridWidth, gameData.GridHeight, 0, 0));
    }
   

}
