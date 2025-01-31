using ServiceLocations;
using SLS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingInitializator : MonoBehaviour, IService
{
    [SerializeField] private List<Building> Buildings;
    private SaveLoadSystem saveLoadSystem;

    public void Initialize()
    {
        saveLoadSystem = ServiceLocations.ServiceLocator.Context.GetSingle<SaveLoadSystem>();
        foreach (var building in Buildings)
        {
            building.Init(saveLoadSystem.saveLoadSystemCache.GetBuildingData(building.BuildingID, 0));
            building.OnLevelChanged += SaveBuilding;
        }
    }

    public void SaveAll()
    {
        foreach (var building in Buildings)
            SaveBuilding(building);
    }
     
    private void SaveBuilding(Building building)
    {
        saveLoadSystem.saveLoadSystemCache.SetBuildingsData(building.BuildingID, building.Level);
    }
}