using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{


    public static BuildManager instance;
    private GameObject buildTower;
    private GameObject selectedTower; //selected tower when clicked on
    private bool sellTower = false;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of Build Manager");
        }

        instance = this;

    }
    //list of towers to build
    public GameObject tower1;
    public GameObject tower2;
    public GameObject minion1;

    public GameObject GetSeletcedTower()
    {
        return selectedTower;
    }

    public void SetSelectedTower(GameObject tower)
    {

        selectedTower = tower;

    }

    public GameObject GetBuildTower()
    {
        return buildTower;
    }

    public void SetBuildTower(GameObject tower)
    {

        buildTower = tower;

    }
    public void SetSellTower(bool decesion)
    {
        sellTower = decesion;
    }

    public bool GetSellTower()
    {
        return sellTower;
    }

    public bool CanBuy(PlayerData playerData, TowerData towerData)
    {
        if (playerData.Resources >= towerData.Cost)
        {
            int newCost = playerData.Resources - towerData.Cost;
            playerData.SetResources(newCost);
            return true;
        }
        else
            return false;

    }

    public bool CanSell(PlayerData playerData, TowerData towerData)
    {
        if (playerData.Resources >= towerData.Cost)
        {
            int newCost = playerData.Resources - towerData.Cost;
            playerData.SetResources(newCost);
            return true;
        }
        else
            return false;

    }

}
