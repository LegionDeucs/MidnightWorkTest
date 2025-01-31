using SLS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializableStorage : Storage
{
    [SerializeField] protected DataID storageID;
    [SerializeField] protected float saveDelay = 1;
    private CoroutineItem saveDelayRoutine;

    public DataID StorageID => storageID;

    public event Action<SerializableStorage> OnStorageNeedSave;

    internal void Init(Dictionary<ItemConfig, int> dictionary)
    {
        itemsInStorage = dictionary;
        currentStoredValue = 0;
        foreach (var item in itemsInStorage)
        {
            currentStoredValue += item.Key.pieceStorageValue * item.Value;
        }

        OnItemChanged += SerializableStorage_OnItemChanged;
    }

    private void SerializableStorage_OnItemChanged(ItemConfig whatChanged, int afterChangeValue) => TryToSave();

    public void TryToSave()
    {
        saveDelayRoutine?.Stop();
        saveDelayRoutine = this.WaitAndDoCoroutine(saveDelay,()=> OnStorageNeedSave?.Invoke(this));
    }
}
