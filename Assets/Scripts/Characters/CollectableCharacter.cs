using SLS;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableCharacter<TMovementController, TAnimatorController> : UnitController<TMovementController, TAnimatorController>, ICollectable
      where TMovementController : UnitMovementController
      where TAnimatorController : UnitAnimatorController
{
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected float defaultDrag;
    [SerializeField] protected Collider mainCollider;

    private DataID dataID;

    public Rigidbody RB => rb;

    public Collider MainCollider => mainCollider;

    public DataID DataID => dataID;

    public event Action<ICollectable> OnCollected;

    public void Collected() => OnCollected?.Invoke(this);

    public void Init(DataID data) => dataID = data;

    public virtual void StartCollecting()
    {
        rb.useGravity = false;
    }

    public virtual void StopCollecting()
    {
        rb.useGravity = true;
        rb.drag = defaultDrag;
    }

    public void UpdateCollecting(float drag, Vector3 force, float scale)
    {
        rb.drag = drag;
        rb.AddForce(force);

        transform.localScale = Vector3.one * scale;
    }
}
