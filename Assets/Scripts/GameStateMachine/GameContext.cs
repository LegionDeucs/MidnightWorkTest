using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour, IStateMachineContext
{
    [SerializeField] private InputSystemProcessor inputAction;
    [SerializeField] private ItemFactory itemFactory;
    [SerializeField] private PlayerObserver playerObserver;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private HUDUI hUDUI;

    [SerializeField] private StorageInitializator storageInitializator;
    [SerializeField] private BuildingInitializator buildingInitializator;

    public GameStateMachine StateMachine { get; private set; }

    private void Awake()
    {
        SetupAdditionalServices();

        StateMachine = new GameStateMachine(this);
    }
    private void Start()
    {
        StateMachine.EnterState<BootGameState>();
    }

    private void SetupAdditionalServices()
    {
        ServiceLocations.ServiceLocator.Context.BindSingle(inputAction);
        ServiceLocations.ServiceLocator.Context.Replace(cameraController);
        ServiceLocations.ServiceLocator.Context.Replace(itemFactory);
        ServiceLocations.ServiceLocator.Context.Replace(hUDUI);
        ServiceLocations.ServiceLocator.Context.Replace(playerObserver);
        ServiceLocations.ServiceLocator.Context.Replace(storageInitializator);
        ServiceLocations.ServiceLocator.Context.Replace(buildingInitializator);
        ServiceLocations.ServiceLocator.Context.Replace(new ColliderDictionary());
    }
}
