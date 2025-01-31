using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDictionary : ServiceLocations.IService
{
    public Dictionary<Collider, ICollectable> collectables;
    public Dictionary<Collider, ThrowInStorage> throwInStorage;

    public ColliderDictionary()
    {
        collectables = new Dictionary<Collider, ICollectable>(); 
        throwInStorage = new Dictionary<Collider, ThrowInStorage>();
    }

    public void RegisterCollectable(ICollectable collectable, Collider mainCollider) => collectables.Add(mainCollider, collectable);
    public void RemoveCollectable(Collider collider) => collectables.Remove(collider);
    public ICollectable GetCollectable(Collider collider) => collectables[collider];

    public void RegisterThrowInStorage(ThrowInStorage collectable, Collider mainCollider) => throwInStorage.Add(mainCollider, collectable);
    public void RemoveThrowInStorage(Collider collider) => throwInStorage.Remove(collider);
    public ThrowInStorage GetThrowInStorage(Collider coll) => throwInStorage[coll];
}
