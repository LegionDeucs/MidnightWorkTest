using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DictionarySerializable<TKey, TValue>
{
    public List<KeyValueData> keyValues;

    [NonSerialized]
    private List<TKey> keys;

    public int Count => keyValues.Count;

    public DictionarySerializable(Dictionary<TKey, TValue> dictionary)
    {
        keyValues = new List<KeyValueData>();
        keys = new List<TKey>();

        foreach (var pair in dictionary)
        {
            keyValues.Add(new KeyValueData(pair.Key, pair.Value));
            keys.Add(pair.Key);
        }
    }

    public DictionarySerializable()
    {
        keyValues = new List<KeyValueData>();
        keys = new List<TKey>();
    }

    //Call after Deserialization
    public DictionarySerializable<TKey, TValue> Init()
    {
        foreach (var pair in keyValues)
            keys.Add(pair.Key);

        return this;
    }

    public Dictionary<TKey, TValue> ToDictionary()
    {
        Dictionary<TKey,TValue> dictionary = new Dictionary<TKey, TValue>();
        foreach (var pairs in keyValues)
            dictionary.Add(pairs.Key, pairs.Value);
        return dictionary;
    }

    public void Add(TKey key, TValue value)
    {
        keyValues.Add(new KeyValueData(key, value));
        keys.Add(key);
    }

    public DictionarySerializable<TKey, TValue>.KeyValueData GetAtIndex(int i) => keyValues[i];

    public bool ContainsKey(TKey key) => keys.Contains(key);

    [Serializable]
    public class KeyValueData
    {
        public TKey Key;
        public TValue Value;

        public KeyValueData(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }

    public TValue this[TKey key]
    {
        get => keyValues[keys.IndexOf(key)].Value;
        set
        {
            int index = keys.IndexOf(key);
            if (index == -1)
                keyValues.Add(new KeyValueData(key, value));
            else
                keyValues[index].Value = value;
        }
    }
}