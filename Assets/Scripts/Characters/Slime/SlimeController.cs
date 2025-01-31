using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeController : CollectableCharacter<SlimeMovementController, SlimeAnimatorController>
{
    [SerializeField] private ItemProducer gelProducer;
    private void Start()
    {
        movementController.StartMovingRandom();
        gelProducer.StartProducingItems();
    }

    public override void StartCollecting()
    {
        base.StartCollecting();

        movementController.StopRandomMoving();
        gelProducer.StopProducing();
    }

    public override void StopCollecting()
    {
        base.StopCollecting();

        movementController.StartMovingRandom();
        gelProducer.StartProducingItems();
    }
}