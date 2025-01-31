using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : UnitController<PlayerMovementController, PlayerAnimatorController>
{
    [SerializeField] private PlayerBackpack playerBackpack;
    [SerializeField] private VacuumController vacuumController;
    [SerializeField] private ReverseVacuumController reverseVacuumController;

    public PlayerBackpack Backpack => playerBackpack;
    public VacuumController VacuumController => vacuumController;
    public ReverseVacuumController ReverseVacuumController => reverseVacuumController;

    private InputSystemProcessor inputManager;
    private ItemsEncyclopedia itemsEncyclopedia;

    private void Start()
    {
        inputManager = ServiceLocations.ServiceLocator.Context.GetSingle<InputSystemProcessor>();
        itemsEncyclopedia = ServiceLocations.ServiceLocator.Context.GetSingle<ItemsEncyclopedia>();

        inputManager.OnChangeVacuumType += InputManager_OnChangeVacuumType;
        inputManager.OnChangeThrowItemType += InputManager_OnChangeThrowItemType;
        inputManager.OnChangeSuckItemType += InputManager_OnChangeSuckItemType;
        vacuumController.StartCollecting();
    }

    private void InputManager_OnChangeSuckItemType() => vacuumController.ChangeCollectableItemType();

    private void InputManager_OnChangeThrowItemType() => reverseVacuumController.SycleCurrentThrowingItem();

    private void InputManager_OnChangeVacuumType(VacuumType vacuumType)
    {
        vacuumController.OnCollected -= VacuumController_OnCollected;

        playerBackpack.OnStorageFull -= PlayerBackpack_OnStoragePackFull;
        playerBackpack.OnStorageHasSpace -= PlayerBackpack_OnStorageHasSpace;

        vacuumController.StopCollecting();
        reverseVacuumController.StopThrowing();

        switch (vacuumType)
        {
            case VacuumType.Vacuum:
                vacuumController.StartCollecting();
                vacuumController.OnCollected += VacuumController_OnCollected;

                playerBackpack.OnStorageFull += PlayerBackpack_OnStoragePackFull;
                playerBackpack.OnStorageHasSpace += PlayerBackpack_OnStorageHasSpace;
                break;
            case VacuumType.ReverseVacuum:
                reverseVacuumController.StartThrowing();
                break;
            default:
                break;
        }
    }

    private void PlayerBackpack_OnStorageHasSpace()
    {
        vacuumController.StartCollecting();
    }

    private void PlayerBackpack_OnStoragePackFull()
    {
        vacuumController.StopCollecting();
    }

    private void VacuumController_OnCollected(ICollectable collectedItem)
    {
        playerBackpack.AddItem(itemsEncyclopedia.GetItemType(collectedItem.DataID.ID));
        collectedItem.Collected();
    }

    private void Update()
    {
        movementController.MoveDirection(inputManager.GetPlayerMoveInput());
    }

    private void FixedUpdate()
    {
        if(playerBackpack.HasSpaceInStorage)
            vacuumController.DoFixedUpdate();
    }
}
