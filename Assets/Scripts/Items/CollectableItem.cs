using SLS;
using System;
using UnityEngine;

public class CollectableItem : MonoBehaviour, ICollectable
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float defaultDrag = 0;
    [SerializeField] private Collider mainCollider;

    private DataID dataID;

    public Rigidbody RB => rb;

    public Collider MainCollider => mainCollider;

    public DataID DataID => dataID;

    public event Action<ICollectable> OnCollected;

    public virtual void Collected() => OnCollected?.Invoke(this);

    public virtual void Init(DataID data) => dataID = data;

    public virtual void StartCollecting()
    {
        rb.useGravity = false;
    }

    public virtual void StopCollecting()
    {
        rb.useGravity = true;
        rb.drag = defaultDrag;
    }

    public virtual void UpdateCollecting(float drag, Vector3 force, float scale)
    {
        rb.drag = drag;
        rb.AddForce(force);

        transform.localScale = Vector3.one * scale;
    }
}