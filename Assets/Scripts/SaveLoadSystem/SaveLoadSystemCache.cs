using System;

namespace SLS
{
    [System.Serializable]
    public class SaveLoadSystemCache
    {
        public int version;
        public int level;

        public DictionarySerializable<string, int> buildingData;
        public DictionarySerializable<string, DictionarySerializable<string, int>> storageData;

        public SaveLoadSystemCache()
        {
            buildingData = new DictionarySerializable<string, int>();
            storageData = new DictionarySerializable<string, DictionarySerializable<string, int>>();
        }

        internal SaveLoadSystemCache Init()
        {
            buildingData.Init();
            storageData.Init();

            return this;
        }

        public int GetBuildingData(DataID buildingID, int defaultValue)
        {
            if (!buildingData.ContainsKey(buildingID.ID))
                buildingData.Add(buildingID.ID, defaultValue);
                
            return buildingData[buildingID.ID];

        }

        public DictionarySerializable<string, int> GetStorageData(DataID storageID, DictionarySerializable<string, int> defaultData)
        {
            if (!storageData.ContainsKey(storageID.ID))
                storageData.Add(storageID.ID, defaultData);

            return storageData[storageID.ID];
        }

        public void SetBuildingsData(DataID buildingID, int level)
        {
            if (buildingData.ContainsKey(buildingID.ID))
                buildingData[buildingID.ID] = level;
            else 
                buildingData.Add(buildingID.ID, level);
        }

        public void SetStorageData(DataID storageID, DictionarySerializable<string, int> dictionarySerializable)
        {
            if (storageData.ContainsKey(storageID.ID))
                storageData[storageID.ID] = dictionarySerializable;
            else
                storageData.Add(storageID.ID, dictionarySerializable);
        }

    }
}
