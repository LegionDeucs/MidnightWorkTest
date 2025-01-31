using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitMovementController : MonoBehaviour
{
    public abstract void MoveDirection(Vector3 moveDirection);
}
