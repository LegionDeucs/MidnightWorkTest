using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowInExchangeStorage : ThrowInStorage
{
    [SerializeField] private List<ExchangeCourse> exchangeCourse;
    [SerializeField] private float itemSpawnDelay;

    protected ItemFactory itemFactory;
    protected List<ItemConfig> acceptableItems;
    protected Dictionary<ItemConfig, ExchangeCourse> courses;

    protected override List<ItemConfig> AcceptableItems => acceptableItems;

    protected override void Start()
    {
        base.Start();

        itemFactory = ServiceLocations.ServiceLocator.Context.GetSingle<ItemFactory>();

        acceptableItems = new List<ItemConfig>();
        courses = new Dictionary<ItemConfig, ExchangeCourse>();

        foreach (var item in exchangeCourse)
        {
            acceptableItems.Add(item.fromItem);
            courses.Add(item.fromItem, item);
        }
    }

    protected override IEnumerator MoveRoutine(ICollectable collectable)
    {
        yield return base.MoveRoutine(collectable);

        WaitForSeconds waitToSpawn = new WaitForSeconds(itemSpawnDelay);
        var course = courses[itemsEncyclopedia.GetItemType(collectable.DataID.ID)];
        itemFactory.DespawnItem(collectable);

        ICollectable item;
        for (int i = 0; i < course.amount; i++)
        {
            yield return waitToSpawn;
            item = itemFactory.SpawnItem(course.toItem);
            item.transform.position = kickFromPoint.position;
            item.RB.AddForce(kickFromPoint.forward * kickForce, ForceMode.VelocityChange);
        }
    }
}

[System.Serializable]
public class ExchangeCourse
{
    public ItemConfig fromItem;
    public ItemConfig toItem;
    public int amount;
}