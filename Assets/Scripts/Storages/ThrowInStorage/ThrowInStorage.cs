using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ThrowInStorage : Storage
{
    [SerializeField] protected float moveSpeed = 5;

    [SerializeField] protected AnimationCurve moveCurve;
    [SerializeField] protected Transform suckInDestinationPoint;
    [SerializeField] protected Transform kickFromPoint;
    [SerializeField] protected float kickForce = 5;



    [SerializeField] protected Collider throwInStorageCollider;

    protected abstract List<ItemConfig> AcceptableItems { get; }

    public override bool CheckCompatibility(ItemConfig item) => itemsEncyclopedia.IsCompatible(AcceptableItems, item);

    protected override void Start()
    {
        base.Start();

        ServiceLocations.ServiceLocator.Context.GetSingle<ColliderDictionary>().RegisterThrowInStorage(this, throwInStorageCollider);
    }

    public override bool AddItem(ItemConfig producedItem, ICollectable collectable)
    {
        StartCoroutine(MoveRoutine(collectable));
        return base.AddItem(producedItem, collectable);
    }

    protected virtual IEnumerator MoveRoutine(ICollectable collectable)
    {
        collectable.RB.isKinematic = true;
        float moveDuration = (collectable.transform.position - suckInDestinationPoint.position).magnitude / moveSpeed;

        Vector3 startPosition = collectable.transform.position;
        float t = 0;

        while (t < moveDuration)
        {
            t += Time.deltaTime;
            collectable.transform.position = Vector3.Lerp(startPosition, suckInDestinationPoint.position, moveCurve.Evaluate(Mathf.InverseLerp(0, moveDuration,t)));
            yield return null;
        }
    }
}
