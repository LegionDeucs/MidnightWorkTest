using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemConfig", menuName = "ScriptableObjects/Items/BuyConfig")]
public class BuyConfig : ScriptableObject
{
    public List<RequiredEntities> requiredEntities;
}

[System.Serializable]
public class RequiredEntities
{
    public ItemConfig itemConfig;
    public int requiredAmount;
}
