using ServiceLocations;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsEncyclopedia : MonoBehaviour, IService
{
    [SerializeField] private List<ItemConfig> itemTypes;
    [SerializeField] private List<ItemConfig> slimes;

    private Dictionary<string, ItemConfig> itemsIDPair;

    public const char SPLITER = '.';

    public float ItemCount => itemTypes.Count;

    private void OnValidate()
    {
        CheckID();
    }

    private void CheckID()
    {
        //TODO crate ID spelling check
    }

    private void Awake()
    {
        itemsIDPair = new Dictionary<string, ItemConfig>();
        foreach (var itemType in itemTypes)
            itemsIDPair.Add(itemType.itemDataID.ID, itemType);
    }

    public ItemConfig GetItemType(string id)
    {
        var idParts = id.Split(SPLITER);
        var itemId = idParts.Last();
        return itemsIDPair[itemId];
    }

    public bool IsSlime(ItemConfig config) => slimes.Contains(config);

    //TODO create type check (for example if it is a slime)
    internal bool IsCompatible(List<ItemConfig> acceptableItems, ItemConfig item) => acceptableItems.Contains(item);

    internal ItemConfig GetItemType(int currentCollectableItem) => itemTypes[currentCollectableItem];
}
