using ServiceLocations;
using SLS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageInitializator : MonoBehaviour, IService
{
    [SerializeField] private List<SerializableStorage> storages;
    private SaveLoadSystem saveLoadSystem;
    private ItemsEncyclopedia itemsEncyclopedia;

    public void Initialize()
    {
        saveLoadSystem = ServiceLocator.Context.GetSingle<SaveLoadSystem>();
        itemsEncyclopedia = ServiceLocator.Context.GetSingle<ItemsEncyclopedia>();

        foreach (var storage in storages)
        {
            storage.Init(ValidateData(saveLoadSystem.saveLoadSystemCache.GetStorageData(storage.StorageID,
            new DictionarySerializable<string, int>())));
            storage.OnStorageNeedSave += SaveStorage;
        }

    }
    private Dictionary<ItemConfig, int> ValidateData(DictionarySerializable<string, int> stored)
    {
        Dictionary<ItemConfig, int> items = new Dictionary<ItemConfig, int>();

        DictionarySerializable<string, int>.KeyValueData data;
        for (int i = 0; i < stored.Count; i++)
        {
            data = stored.GetAtIndex(i);
            items.Add(itemsEncyclopedia.GetItemType(data.Key), data.Value);
        }
        return items;
    }

    public void SaveAll()
    {
        foreach (var storage in storages)
            SaveStorage(storage);
    }

    public void SaveStorage(SerializableStorage storage)
    {
        saveLoadSystem.saveLoadSystemCache.SetStorageData(storage.StorageID, ConvertToStorageSaveData(storage.GetSaveData(), storage));
    }

    private DictionarySerializable<string, int> ConvertToStorageSaveData(Dictionary<ItemConfig, int> storageData, SerializableStorage storage)
    {
        Dictionary<string, int> storageSaveData = new Dictionary<string, int>();

        foreach (var item in storageData)
            storageSaveData.Add(storage.StorageID.ID + ItemsEncyclopedia.SPLITER + item.Key.itemDataID.ID, item.Value);

        return new DictionarySerializable<string, int>(storageSaveData);
    }
}
