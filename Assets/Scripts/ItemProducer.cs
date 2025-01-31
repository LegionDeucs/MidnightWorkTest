using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemProducer : MonoBehaviour
{
    [SerializeField] private ItemConfig producedItem;

    [SerializeField] private Vector2 productionDelay;

    [SerializeField] private int producedItemCount;
    [SerializeField] private float dropOutDelay = 0.05f;
    [SerializeField] private Storage collatableStorage;
    [SerializeField] private List<Transform> creationPoint;
    [SerializeField] private float dropOutForce;

    private ItemFactory itemFactory;
    private Coroutine productionRoutine;

    public event System.Action<float> OnProductionTick;

    private void Start()
    {
        itemFactory = ServiceLocations.ServiceLocator.Context.GetSingle<ItemFactory>();
    }

    public void StartProducingItems()
    {
        StopProducing();

        productionRoutine = StartCoroutine(ProductionCoroutine(Random.Range(productionDelay.x, productionDelay.y)));
    }

    private IEnumerator ProductionCoroutine(float productionDuration)
    {
        float t = 0;
        var waitForSpawnDelay = new WaitForSeconds(dropOutDelay);
        while (t < productionDuration)
        {
            yield return null;
            t += Time.deltaTime;
            OnProductionTick?.Invoke(Mathf.InverseLerp(0, productionDuration, t));
        }

        if (collatableStorage == null || collatableStorage.HasSpaceInStorage)
            for (int i = 0; i < producedItemCount; i++)
            {
                yield return waitForSpawnDelay;
                SpawnItem();
            }

        StartProducingItems();
    }

    public void SpawnItem()
    {
        if(collatableStorage == null || !collatableStorage.IsDataType)
        {
            ICollectable collectable = itemFactory.SpawnItem(producedItem);
            collectable.transform.position = creationPoint.GetRandom().position;
            var throwDirection = Random.onUnitSphere;
            throwDirection = throwDirection.WithY(Mathf.Abs(throwDirection.y));
            collectable.RB.AddForce(throwDirection * dropOutForce, ForceMode.VelocityChange);
            if (collatableStorage != null)
                collatableStorage.AddItem(producedItem, collectable);
        }
        else
            collatableStorage.AddItem(producedItem);
    }

    internal void StopProducing()
    {
        if (productionRoutine != null)
            StopCoroutine(productionRoutine);
    }
}
