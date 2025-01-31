using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    [SerializeField] private ResourceItemUI UIItemPrefab;
    [SerializeField] private Transform ItemsHolder;

    private Dictionary<ItemConfig, ResourceItemUI> items;

    private void Awake()
    {
        items = new Dictionary<ItemConfig, ResourceItemUI>();
    }

    public void SetItem(ItemConfig item, int count)
    {
        if(items.ContainsKey(item))
        {
            items[item].SetCount(count);
            return;
        }
        else
        {
            ResourceItemUI uiItem = Instantiate(UIItemPrefab);
            items.Add(item, uiItem);

            uiItem.transform.SetParent(ItemsHolder);
            uiItem.SetCount(count);
            uiItem.SetIcon(item.itemSprite);
        }
    }
}
