using SLS;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Building : MonoBehaviour
{
    [SerializeField] protected DataID saveID;
    [SerializeField] protected List<BuildingLevel> buildingLevels;

    protected int currentLevel;

    public DataID BuildingID => saveID;

    public int Level => currentLevel;

    public event Action<Building> OnLevelChanged;

    public virtual void Init(int level)
    {
        for (int i = 0; i < buildingLevels.Count; i++)
            buildingLevels[i].purchaseAnimation?.Init();
        SetLevel(level);
    }

    private void SetLevel(int levelNumber)
    {
        currentLevel = levelNumber;

        for (int i = 0; i < levelNumber; i++)
            buildingLevels[i].purchaseAnimation?.ForceShow();

        for (int i = levelNumber + 1; i < buildingLevels.Count; i++)
            buildingLevels[i].purchaseAnimation?.ForceHide();
    }

    protected void OpenLevel(int levelNumber)
    {
        buildingLevels[levelNumber -1].purchaseAnimation?.PlayAnimation();
        currentLevel = levelNumber;
        OnLevelChanged?.Invoke(this);
    }
}

[System.Serializable]
public class BuildingLevel
{
    public List<Transform> enableElements;
    public List<Transform> disableElements;

    public VisualAnimator purchaseAnimation;
}