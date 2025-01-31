using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackpack : SerializableStorage
{
    internal Dictionary<ItemConfig, int> GetItems() => itemsInStorage;
}
