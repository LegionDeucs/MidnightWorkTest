using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ReverseVacuumController : MonoBehaviour
{
    [SerializeField] private Storage vacuumStorage;
    [SerializeField] private ColliderTrigger colliderTrigger;

    [SerializeField] private Transform spawnPoint;

    [SerializeField] float throwRate = 2;

    private ColliderDictionary colliderDictionary;
    private ItemFactory itemFactory;
    private ItemConfig currentThrowItemConfig;

    public event System.Action<ItemConfig> OnThrowingItemChanged;

    private int currentThrowingItemCycle;
    private Coroutine throwRoutone;

    private void Start()
    {
        colliderDictionary = ServiceLocations.ServiceLocator.Context.GetSingle<ColliderDictionary>();
        itemFactory = ServiceLocations.ServiceLocator.Context.GetSingle<ItemFactory>();
    }

    public void StartThrowing()
    {
        colliderTrigger.OnTriggerEventEnter += ColliderTrigger_OnTriggerEventEnter;
        colliderTrigger.OnTriggerEventLeave += ColliderTrigger_OnTriggerEventLeave;
    }

    private void ColliderTrigger_OnTriggerEventLeave(Collider obj)
    {
        if (throwRoutone != null)
            StopCoroutine(throwRoutone);
    }

    public void SycleCurrentThrowingItem()
    {
        List<ItemConfig> itemTypes = vacuumStorage.GetAllItemsType();
        if (itemTypes.Count == 0)
            return;

        currentThrowingItemCycle = (int)Mathf.Repeat(currentThrowingItemCycle +1, itemTypes.Count);
        currentThrowItemConfig = itemTypes[currentThrowingItemCycle];
        OnThrowingItemChanged?.Invoke(currentThrowItemConfig);

    }

    private void ColliderTrigger_OnTriggerEventEnter(Collider coll)
    {
        if (vacuumStorage.Contains(currentThrowItemConfig, out int count))
        {
            ThrowInStorage storage = colliderDictionary.GetThrowInStorage(coll);

            if (throwRoutone != null)
                StopCoroutine(throwRoutone);

            throwRoutone = StartCoroutine(ThrowRoutine(storage, count));
        }
        else
            SycleCurrentThrowingItem();
    }

    private IEnumerator ThrowRoutine(ThrowInStorage storage, int count)
    {
        WaitForSeconds waitForThrow = new WaitForSeconds(1 / throwRate);
        ICollectable item;
        for (int i = 0; i < count && storage.CheckCompatibility(currentThrowItemConfig); i++)
        {
            item = itemFactory.SpawnItem(currentThrowItemConfig);
            item.transform.position = spawnPoint.position;
            vacuumStorage.RemoveItem(currentThrowItemConfig);
            yield return waitForThrow;
            storage.AddItem(currentThrowItemConfig, item);
        }
    }
    
    public void StopThrowing()
    {
        colliderTrigger.OnTriggerEventEnter -= ColliderTrigger_OnTriggerEventEnter;
        colliderTrigger.OnTriggerEventLeave -= ColliderTrigger_OnTriggerEventLeave;

        if (throwRoutone != null)
            StopCoroutine(throwRoutone);
    }
}
