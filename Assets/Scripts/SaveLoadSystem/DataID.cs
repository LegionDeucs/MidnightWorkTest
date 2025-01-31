using System;
using UnityEngine;

namespace SLS
{
    [CreateAssetMenu(fileName = "SaveData", menuName = "ScriptableObjects/SaveData")]
    public class DataID : ScriptableObject
    {
        public string ID;
    }
}