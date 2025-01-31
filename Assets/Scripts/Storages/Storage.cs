using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public delegate void OnStorageChanged(ItemConfig whatChanged, int afterChangeValue);
public class Storage : MonoBehaviour
{
    [SerializeField] protected int Capacity;
    [SerializeField] protected bool isDataType;

    protected Dictionary<ItemConfig, int> itemsInStorage;
    protected int currentStoredValue;

    public bool HasSpaceInStorage => Capacity==-1 || currentStoredValue < Capacity;

    public bool IsDataType => isDataType;

    public Dictionary<ItemConfig, int> GetSaveData() => itemsInStorage;
    protected Dictionary<ItemConfig, List<ICollectable>> collectables;
    protected ItemsEncyclopedia itemsEncyclopedia;

    public event OnStorageChanged OnItemChanged;
    public event System.Action OnStorageFull;
    public event System.Action OnStorageHasSpace;

    public virtual bool CheckCompatibility(ItemConfig item) => true;

    protected virtual void Awake()
    {
        collectables = new Dictionary<ItemConfig, List<ICollectable>>();
        itemsInStorage = new Dictionary<ItemConfig, int>();
    }

    protected virtual void Start()
    {
        itemsEncyclopedia = ServiceLocations.ServiceLocator.Context.GetSingle<ItemsEncyclopedia>();
    }

    public virtual bool AddItem(ItemConfig item)
    {
        if (!HasSpaceInStorage)
            return false;

        if (itemsInStorage.ContainsKey(item))
            itemsInStorage[item]++;
        else
            itemsInStorage.Add(item, 1);

        currentStoredValue += item.pieceStorageValue;

        OnItemChanged?.Invoke(item, itemsInStorage[item]);

        if (!HasSpaceInStorage)
            OnStorageFull?.Invoke();

        return true;
    }

    public virtual void RemoveItem(ItemConfig item, int count = 1)
    {
        if (!itemsInStorage.ContainsKey(item) && itemsInStorage[item] > 0)
            return;

        currentStoredValue -= item.pieceStorageValue;
        count = Mathf.Clamp(count, 0, itemsInStorage[item]);

        itemsInStorage[item]-= count;
        int left = itemsInStorage[item];
        if (left == 0)
            itemsInStorage.Remove(item);

        if (HasSpaceInStorage)
            OnStorageHasSpace?.Invoke();

        OnItemChanged?.Invoke(item, left);
    }

    public virtual bool AddItem(ItemConfig producedItem, ICollectable collectable)
    {
        if (!AddItem(producedItem))
            return false;

        if (collectables.ContainsKey(producedItem))
            collectables[producedItem].Add(collectable);
        else
            collectables.Add(producedItem, new List<ICollectable>() { collectable });

        collectable.OnCollected += Collectable_OnCollected;
        return true;
    }

    private void Collectable_OnCollected(ICollectable collectable)
    {
        collectable.OnCollected -= Collectable_OnCollected;
        RemoveItem(collectable);
    }

    public virtual void RemoveItem(ICollectable collectable)
    {
        ItemConfig itemC = itemsEncyclopedia.GetItemType(collectable.DataID.ID);
        RemoveItem(itemC);
        if(!isDataType)
        collectables[itemC].Remove(collectable);
    }

    public bool Contains(ItemConfig currentThrowItemConfig, out int count)
    {
        count = 0;
        bool exist = itemsInStorage.ContainsKey(currentThrowItemConfig);
        if (exist)
            count = itemsInStorage[currentThrowItemConfig];
        return exist && count > 0;
    }

    internal List<ItemConfig> GetAllItemsType() => itemsInStorage.Keys.ToList();
}