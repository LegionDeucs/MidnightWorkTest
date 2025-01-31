using System;
using UnityEngine;

public class ColliderTrigger : Trigger<Collider>
{
    [SerializeField] protected LayerMask interactionLayer;

    public override event Action<Collider> OnTriggerEventEnter;
    public override event Action<Collider> OnTriggerEventLeave;

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (interactionLayer.Includes(other.gameObject.layer))
            OnTriggerEventEnter?.Invoke(other);
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (interactionLayer.Includes(other.gameObject.layer))
            OnTriggerEventLeave?.Invoke(other);
    }
}
