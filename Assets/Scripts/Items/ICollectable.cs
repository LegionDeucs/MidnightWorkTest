using SLS;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectable
{
    public event System.Action<ICollectable> OnCollected;

    public Transform transform { get; }
    public Rigidbody RB { get; }
    public Collider MainCollider { get; }

    public DataID DataID { get; }

    void Collected();

    public void Init(DataID data);
    public void StartCollecting();
    public void StopCollecting();

    public void UpdateCollecting(float drag, Vector3 force, float scale);
}
