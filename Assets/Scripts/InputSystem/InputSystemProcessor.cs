using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ServiceLocations;
using System;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem;

public enum VacuumType { Vacuum,  ReverseVacuum}
public class InputSystemProcessor : MonoBehaviour, IService
{
    private PlayerInputAction inputAction;

    public event System.Action<VacuumType> OnChangeVacuumType;
    public event System.Action OnChangeThrowItemType;
    public event System.Action OnChangeSuckItemType;

    private void Awake()
    {
        inputAction = new PlayerInputAction();
    }

    private void OnEnable()
    {
        inputAction.PlayerVacuum.Enable();

        inputAction.PlayerVacuum.TransitionToThrow.performed += TransitionToThrow_performed;
        inputAction.PlayerThrow.TransitionToVacuum.performed += TransitionToVacuum_performed;

        EnhancedTouchSupport.Enable();
        TouchSimulation.Enable();
    }

    private void TransitionToVacuum_performed(InputAction.CallbackContext obj)
    {
        inputAction.PlayerVacuum.Enable();
        inputAction.PlayerVacuum.ChangePickUpItemType.performed += ChangePickUpItemType_performed;
        inputAction.PlayerThrow.Disable();
        inputAction.PlayerThrow.ChangeThowItemType.performed -= ChangeThrowItemType_performed;
        OnChangeVacuumType?.Invoke(VacuumType.Vacuum);
    }

    private void ChangeThrowItemType_performed(InputAction.CallbackContext obj)
    {
        OnChangeThrowItemType?.Invoke();
    }

    private void ChangePickUpItemType_performed(InputAction.CallbackContext obj)
    {
        OnChangeSuckItemType?.Invoke();
    }

    private void TransitionToThrow_performed(InputAction.CallbackContext obj)
    {
        inputAction.PlayerVacuum.Disable();
        inputAction.PlayerThrow.Enable();

        inputAction.PlayerVacuum.ChangePickUpItemType.performed -= ChangePickUpItemType_performed;
        inputAction.PlayerThrow.ChangeThowItemType.performed += ChangeThrowItemType_performed;

        OnChangeVacuumType?.Invoke(VacuumType.ReverseVacuum);
    }

    private void OnDisable()
    {
        inputAction.PlayerVacuum.Disable();
        inputAction.PlayerThrow.Disable();

        EnhancedTouchSupport.Disable();
        TouchSimulation.Disable();
    }

    public Vector3 GetPlayerMoveInput() => inputAction.PlayerThrow.enabled? 
                                           inputAction.PlayerThrow.Move.ReadValue<Vector2>().YtoZ():
                                           inputAction.PlayerVacuum.Move.ReadValue<Vector2>().YtoZ();
}
