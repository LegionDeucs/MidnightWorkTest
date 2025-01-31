using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseStorage : ThrowInStorage
{
    private List<ItemConfig> acceptableItems;
    private BuyConfig currentBuyConfig;

    protected override List<ItemConfig> AcceptableItems => acceptableItems;
    public event System.Action<PurchaseStorage> OnBuyConfigMatched;

    public void SetupCost(BuyConfig buyConfig)
    {
        acceptableItems = new List<ItemConfig>();
        currentBuyConfig = buyConfig;

        foreach (var item in buyConfig.requiredEntities)
        {
            if (!itemsInStorage.ContainsKey(item.itemConfig) || itemsInStorage[item.itemConfig] < item.requiredAmount)
                acceptableItems.Add(item.itemConfig);
        }

        CheckMatch(null, 0);
        OnItemChanged += CheckMatch;
    }

    private void CheckMatch(ItemConfig whatChanged, int afterChangeValue)
    {
        foreach (var item in currentBuyConfig.requiredEntities)
            if (acceptableItems.Contains(item.itemConfig) && itemsInStorage.ContainsKey(item.itemConfig) && itemsInStorage[item.itemConfig] >= item.requiredAmount)
            {
                RemoveItem(item.itemConfig, item.requiredAmount);
                acceptableItems.Remove(item.itemConfig);
            }

        if (acceptableItems.Count == 0)
        {
            OnItemChanged -= CheckMatch;
            OnBuyConfigMatched?.Invoke(this);
        }
    }

    protected override IEnumerator MoveRoutine(ICollectable collectable)
    {
        yield return base.MoveRoutine(collectable);
    }
}
