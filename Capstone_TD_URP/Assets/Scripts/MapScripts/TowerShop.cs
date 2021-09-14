using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShop : MonoBehaviour
{

    BuildManager buildManager;
    public GameObject TowerPanel;

    void Start() 
    {
        buildManager = BuildManager.instance;
    
    }

    public void PurchaseTower1() 
    {
        Debug.Log("Tower1 Purchased");
        buildManager.SetBuildTower(buildManager.tower1);
        TowerPanel.SetActive(false);
    }

    public void PurchaseTower2()
    {
        Debug.Log("Tower2 Purchased");
        buildManager.SetBuildTower(buildManager.tower2);
        TowerPanel.SetActive(false);
    }

    public void SellTower() 
    {
        Debug.Log("Selling Tower");
        buildManager.SetSellTower(true);
        TowerPanel.SetActive(false);
    }
    public void DestoryTower() {
        
        if (buildManager.GetSeletcedTower() != null)
        {
            GameObject towertoSell = buildManager.GetSeletcedTower();
            Debug.Log("Destory Tower");
            Destroy(towertoSell);
            BuildManager.instance.SetSellTower(false);
            
        }
        TowerPanel.SetActive(false);
    }



}
