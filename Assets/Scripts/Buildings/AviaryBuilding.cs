using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AviaryBuilding : Building
{
    [SerializeField] private Storage storage;
    [SerializeField] private TileObject tileObject;

    private void Start()
    {
        tileObject.OnPurchased += TileObject_OnPurchased;
    }
    public override void Init(int level)
    {
        base.Init(level);
        tileObject.Init(level);
    }

    private void TileObject_OnPurchased()
    {
        currentLevel++;
        OpenLevel(currentLevel);
        if(currentLevel < buildingLevels.Count)
            tileObject.SetLevel(currentLevel);
    }
}
