using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObjectThrowInStorage : ThrowInStorage
{
    [SerializeField] protected List<ItemConfig> acceptableItems;

    protected override List<ItemConfig> AcceptableItems => acceptableItems;

    protected override IEnumerator MoveRoutine(ICollectable collectable)
    {
        yield return base.MoveRoutine(collectable);

        collectable.transform.position = kickFromPoint.position;
        collectable.transform.forward = kickFromPoint.forward.WithY(0);

        collectable.RB.isKinematic = false;
        collectable.RB.AddForce(kickFromPoint.forward * kickForce, ForceMode.VelocityChange);
    }
}
