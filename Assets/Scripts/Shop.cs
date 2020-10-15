using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;
    public TowerBlueprint WizardTower;
    public TowerBlueprint defaultTower;

    private void Start()
    {
       buildManager = BuildManager.instance;
    }

    public void BuildWizardTower()
    {
        buildManager.BuildTower(WizardTower);
    }

}
