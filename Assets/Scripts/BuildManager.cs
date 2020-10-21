using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{

    private GameObject currentTower = null; 

    public static BuildManager instance;
    public Transform defaultBuildLocation;

    public Transform MinPoints;
    public Transform MaxPoints;
    public Camera camera;
    public Boolean isBuilding = false;
    public GameObject placeButton;
    public GameObject deselectButton;
    private int currentTowerCost;

    private void Awake()
    {
        placeButton.SetActive(false);
        deselectButton.SetActive(false);
        if(instance != null)
        {
            Debug.LogError("Build Manager Already Exists");
            return;
        }
        instance = this;
    }

    public void BuildTower(TowerBlueprint towerBlueprint)
    {


        currentTowerCost = towerBlueprint.cost;
        GameObject builtTower = (GameObject)Instantiate(towerBlueprint.towerPrefab, defaultBuildLocation.position, Quaternion.identity);

        enablePlaceButton();
        setCurrentTurret(builtTower);
            
        
    }

    public void enablePlaceButton()
    {
        placeButton.SetActive(true);
        deselectButton.SetActive(true);
    }

    public void setCurrentTurret(GameObject towerBlueprint)
    {
        currentTower = towerBlueprint;
    }

    public void buildCurrentTower()
    {
        if(currentTower.GetComponent<TurretGeneral>().validLocationToBuild)
        {
            currentTower.GetComponent<TurretGeneral>().isPlaced = true;
            placeButton.SetActive(false);
            deselectButton.SetActive(false);
            PlayerStats.Money -= currentTowerCost;
            currentTower.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
            currentTower.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
            currentTower.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
            currentTower = null;
            isBuilding = false;
            currentTowerCost = 0;
        }
    }

    public void DeselectCurrontTower()
    {
        Destroy(currentTower);
        isBuilding = false;
        currentTower = null;
        currentTowerCost = 0;
        placeButton.SetActive(false);
        deselectButton.SetActive(false);
    }
}
