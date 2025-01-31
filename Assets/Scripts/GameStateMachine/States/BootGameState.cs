using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootGameState : GameBaseState
{
    private StorageInitializator storageInitializator;
    private BuildingInitializator buildingInitializator;

    public BootGameState(GameContext context) : base(context)
    {
        storageInitializator = ServiceLocations.ServiceLocator.Context.GetSingle<StorageInitializator>();
        buildingInitializator = ServiceLocations.ServiceLocator.Context.GetSingle<BuildingInitializator>();
    }

    public override void Dispose()
    {
        
    }

    public override void OnStateEnter()
    {
        storageInitializator.Initialize();
        buildingInitializator.Initialize();

        Context.StateMachine.EnterState<InWorldState>();
    }

    public override void OnStateExit()
    {
        
    }
}
