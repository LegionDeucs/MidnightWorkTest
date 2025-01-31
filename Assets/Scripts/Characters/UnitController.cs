using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitController<TMovementController, TAnimatorController> : MonoBehaviour where TMovementController : UnitMovementController
    where TAnimatorController : UnitAnimatorController   
{
    [SerializeField] protected TMovementController movementController;
    [SerializeField] protected TAnimatorController animatorController;

}
