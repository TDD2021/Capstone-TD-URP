using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShop : MonoBehaviour
{

    BuildManager buildManager;

    void Start() 
    {
        buildManager = BuildManager.instance;
    
    }

    public void PurchaseTower1() 
    {
        Debug.Log("Tower1 Purchased");
        buildManager.SetBuildTower(buildManager.tower1);    
    }

    public void PurchaseTower2()
    {
        Debug.Log("Tower2 Purchased");
        buildManager.SetBuildTower(buildManager.tower2);
    }

    public void SellTower() 
    {
        Debug.Log("Selling Tower");
        buildManager.SetSellTower(true);
    }



}
