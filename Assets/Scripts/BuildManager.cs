using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    public static BuildManager instance;
    public Transform defaultBuildLocation;

    public Transform MinPoints;
    public Transform MaxPoints;
    public Camera camera;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Build Manager Already Exists");
            return;
        }
        instance = this;
    }

    private Boolean enoughMoney(TowerBlueprint towerBlueprint) 
    { 
        return PlayerStats.Money >= towerBlueprint.cost; 
    } 
    public void BuildTower(TowerBlueprint towerBlueprint)
    {
        if (!enoughMoney(towerBlueprint))
        {
            Debug.Log("NOT ENOUGH MONEY");
            return;
        }

        GameObject builtTower = (GameObject)Instantiate(towerBlueprint.towerPrefab, defaultBuildLocation.position, Quaternion.identity);
            
        
    }
}
