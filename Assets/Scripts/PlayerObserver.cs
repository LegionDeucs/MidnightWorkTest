using ServiceLocations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObserver : MonoBehaviour, IService
{
    [SerializeField] private PlayerController playerController;

    public PlayerController PlayerController => playerController;

    public event OnStorageChanged OnPlayerBackpackChanged;

    public event System.Action<ItemConfig> OnSuckingItemChanged;
    public event System.Action<ItemConfig> OnThrowingItemChanged;

    private void Start()
    {
        playerController.Backpack.OnItemChanged += Backpack_OnItemChanged;
        playerController.VacuumController.OnCollectableItemChanged += VacuumController_OnCollectableItemChanged;
        playerController.ReverseVacuumController.OnThrowingItemChanged += ReverseVacuumController_OnThrowingItemChanged; ;
    }

    private void ReverseVacuumController_OnThrowingItemChanged(ItemConfig item) => OnThrowingItemChanged?.Invoke(item);

    private void VacuumController_OnCollectableItemChanged(ItemConfig item) => OnSuckingItemChanged?.Invoke(item);

    private void Backpack_OnItemChanged(ItemConfig whatChanged, int afterChangeValue) => OnPlayerBackpackChanged?.Invoke(whatChanged, afterChangeValue);

    internal void PingBackPack()
    {
        var items = playerController.Backpack.GetItems();

        foreach (var item in items)
            OnPlayerBackpackChanged?.Invoke(item.Key, item.Value);
    }
}
