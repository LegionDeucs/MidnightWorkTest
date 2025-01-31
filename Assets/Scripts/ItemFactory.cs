using ServiceLocations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFactory : MonoBehaviour, IService
{
    private ColliderDictionary colliderDictionary;

    private void Start()
    {
        colliderDictionary = ServiceLocator.Context.GetSingle<ColliderDictionary>();
    }
    internal ICollectable SpawnItem(ItemConfig producedItem)
    {
        //TODO change to item pooling
        var item = Instantiate(producedItem.itemPrefab).GetComponent<ICollectable>();
        item.Init(producedItem.itemDataID);
        colliderDictionary.RegisterCollectable(item, item.MainCollider);
        item.OnCollected += DespawnItem;
        item.transform.SetParent(transform);
        return item;
    }

    public void DespawnItem(ICollectable collectable)
    {
        colliderDictionary.RemoveCollectable(collectable.MainCollider);
        collectable.OnCollected -= DespawnItem;

        Destroy(collectable.transform.gameObject);
    }
}
