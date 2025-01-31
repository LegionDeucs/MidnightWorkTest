using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedInteraction : ColliderTrigger
{
    [SerializeField] protected float interactionDelay;
    private CoroutineItem delayedInteractionRoutine;

    public event System.Action<Collider> OnInteractionTriggered;

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);


        if (interactionLayer.Includes(other.gameObject.layer))
           delayedInteractionRoutine = this.WaitAndDoCoroutine(interactionDelay, ()=> OnInteractionTriggered?.Invoke(other));
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        delayedInteractionRoutine?.Stop();
    }
}
