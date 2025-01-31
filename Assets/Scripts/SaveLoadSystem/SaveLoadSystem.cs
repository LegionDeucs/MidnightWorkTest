using ServiceLocations;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace SLS
{
    public class SaveLoadSystem : IService
    {
        private const string FILE_PATH = "SaveData.saveS";
        //private Dictionary<string, object> cache;
        private JSonDataExtractor jSonDataExtractor;

        public SaveLoadSystemCache saveLoadSystemCache;
        public SaveLoadSystem()
        { 
            //cache = new Dictionary<string, object>();
        }

        public void Init()
        {
            jSonDataExtractor = new JSonDataExtractor(FILE_PATH);
                
            if(jSonDataExtractor.Exist())
                saveLoadSystemCache = jSonDataExtractor.GetData();
            else
                saveLoadSystemCache = new SaveLoadSystemCache().Init();
            
            //cache = jSonDataExtractor.GetData().ToDictionary();
        }

        //public TDataType GetData<TDataType>(DataID id, TDataType defaultData)
        //{
        //    if (cache.ContainsKey(id.ID))
        //    {
        //        if(cache[id.ID] == null)
        //            cache[id.ID] = defaultData;
        //        return (TDataType)cache[id.ID];
        //    }

        //    cache.Add(id.ID, defaultData);
        //    return defaultData;
        //}

        //public void SetData<TData>(DataID id, TData data ) => cache[id.ID] = data;

        public void Save()
        {
            if (jSonDataExtractor == null)
                return;

            jSonDataExtractor = new JSonDataExtractor(FILE_PATH);
            jSonDataExtractor.SaveData(saveLoadSystemCache);
            //jSonDataExtractor.SaveData(new DictionarySerializable<string, object>(cache));
        }
    }

    public class JSonDataExtractor
    {
        private string filePath;
        public JSonDataExtractor(string filePath)
        {
            this.filePath = filePath;
        }

        public SaveLoadSystemCache GetData()
        {
             var serializedData = Read(filePath);
            var data = JsonUtility.FromJson<SaveLoadSystemCache>(serializedData);
            return data == null ? new SaveLoadSystemCache().Init() : data;
        }

        public void SaveData(SaveLoadSystemCache data)
        {
            Write(filePath, JsonUtility.ToJson(data));
        }

        private void Write(string filePath, string data)
        {
            try
            {
                File.WriteAllText(filePath, data);
            }
            catch (IOException ex)
            {
                Debug.LogError($"FileDataStreamer: Error writing to file at {filePath}. Exception: {ex.Message}");
            }
        }

        private string Read(string filePath)
        {
            try
            {
                return File.Exists(filePath) ? File.ReadAllText(filePath) : null;
            }
            catch (IOException ex)
            {
                Debug.LogError($"FileDataStreamer: Error reading from file at {filePath}. Exception: {ex.Message}");
                return null;
            }
        }

        internal bool Exist() => File.Exists(filePath);
    }
}
