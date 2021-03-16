﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;
    private GameObject buildTower;
   

    void Awake() 
    {
        if (instance != null)
        {
            Debug.LogError("More than one instance of BuildManager");
        }

        instance = this;
        
    }
    //list of towers to build
    public GameObject tower1;
    public GameObject tower2;

    public GameObject minion1;

    public GameObject GetBuildTower() 
    {
        return buildTower;
    }

    public void SetBuildTower(GameObject tower) 
    {

        buildTower = tower;
    
    }

}