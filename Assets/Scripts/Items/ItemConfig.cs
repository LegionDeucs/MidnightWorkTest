using SLS;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemConfig", menuName = "ScriptableObjects/Items/ItemConfig")]
public partial class ItemConfig : ScriptableObject
{
    public Transform itemPrefab;
    public DataID itemDataID;
    public int pieceStorageValue;
    public Sprite itemSprite;
}
