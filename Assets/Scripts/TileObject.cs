using JetBrains.Annotations;
using SLS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    [SerializeField] private List<BuyConfig> upgradeConfigs;
    [SerializeField] private PurchaseStorage purchaseStorage;

    public BuyConfig currentConfig { get; private set; }

    public event System.Action OnPurchased;

    public void Init(int upgradeCount)
    {
        if(upgradeCount < upgradeConfigs.Count)
        {
            SetupCurrentConfig(upgradeConfigs[upgradeCount]);
        }
    }

    public void SetLevel(int nextLevel)
    {
        SetupCurrentConfig(upgradeConfigs[nextLevel]);
    }

    private void SetupCurrentConfig(BuyConfig buyConfig)
    {
        currentConfig = buyConfig;
        purchaseStorage.SetupCost(buyConfig);
        purchaseStorage.OnBuyConfigMatched += PurchaseStorage_OnBuyConfigMatched;
    }

    private void PurchaseStorage_OnBuyConfigMatched(PurchaseStorage obj)
    {
        purchaseStorage.OnBuyConfigMatched -= PurchaseStorage_OnBuyConfigMatched;
        OnPurchased?.Invoke();
    }
}
