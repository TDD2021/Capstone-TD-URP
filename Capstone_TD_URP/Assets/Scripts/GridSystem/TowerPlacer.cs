using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    GameObject checker;
    private Grid3D<GridData> grid;
    private GridData towerSpace;

    private int towerId;

    private int towerX;
    private int towerZ;

    private bool isInitialized = false;

    //GridData gridSpace;

    [SerializeField] private List<TowerData> towerDataList;
    private TowerData towerData;

    // Start is called before the first frame update
    void Start()
    {
        checker = gameObject.transform.GetChild(0).gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!checker.GetComponent<BuildCheckerScript>().Obstructed && isInitialized)
        {
            grid.GetXZ(gameObject.transform.position, out towerX, out towerZ);

            towerSpace = grid.GetGridObject(towerX, towerZ);

            if (towerId < towerDataList.Count && !towerSpace.IsObstructed(gameObject.transform.GetChild(0).transform))
            {
                Debug.Log("(Placing) Tower id: " + towerId);
                towerData = towerDataList[towerId];
                Transform builtTransform = Instantiate(towerData.Prefab, grid.GetWorldPosition(towerX, towerZ), Quaternion.identity);
                towerSpace.SetTransform(builtTransform);

                Destroy(gameObject);
            }
        }
    }

    public void Initialize(ref Grid3D<GridData> grid, int towerId)
    {
        this.towerId = towerId;
        this.grid = grid;

        isInitialized = true;
    }
}
