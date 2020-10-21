using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    public TowerBlueprint WizardTower;
    public TowerBlueprint IceTower;
    public TowerBlueprint BombTower;
    public TowerBlueprint FastTower;

    private void Start()
    {
       buildManager = BuildManager.instance;
    }

    public void BuildWizardTower()
    {
        if (!buildManager.isBuilding)
        {
            if (PlayerStats.Money < WizardTower.cost)
                return;
            buildManager.isBuilding = true;
            buildManager.BuildTower(WizardTower);
        }
    }


    public void BuildIceTower()
    {
        if (!buildManager.isBuilding)
        {
            if (PlayerStats.Money < IceTower.cost)
                return;
            buildManager.isBuilding = true;
            buildManager.BuildTower(IceTower);
        }
    }

    public void BuildBombTower()
    {
        if(!buildManager.isBuilding)
        {
            if (PlayerStats.Money < BombTower.cost)
                return;
            buildManager.isBuilding = true;
            buildManager.BuildTower(BombTower);
        }
    }

    public void BuildFastTower()
    {
        if(!buildManager.isBuilding)
        {
            if (PlayerStats.Money < FastTower.cost)
                return;
            buildManager.isBuilding = true;
            buildManager.BuildTower(FastTower);
        }
    }
}
