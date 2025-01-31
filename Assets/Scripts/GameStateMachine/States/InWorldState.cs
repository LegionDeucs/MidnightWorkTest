using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InWorldState : GameBaseState
{
    private PlayerObserver playerObserver;
    private CameraController cameraController;
    private HUDUI hud;

    public InWorldState(GameContext context) : base(context)
    {
        playerObserver = ServiceLocations.ServiceLocator.Context.GetSingle<PlayerObserver>();
        cameraController = ServiceLocations.ServiceLocator.Context.GetSingle<CameraController>();
        hud = ServiceLocations.ServiceLocator.Context.GetSingle<HUDUI>();
    }

    public override void Dispose()
    {
        OnStateExit();
    }

    public override void OnStateEnter()
    {
        cameraController.SetupTarget(playerObserver.PlayerController.transform);
        playerObserver.OnPlayerBackpackChanged += PlayerObserver_OnPlayerBackpackChanged;
        playerObserver.PingBackPack();
    }

    private void PlayerObserver_OnPlayerBackpackChanged(ItemConfig whatChanged, int afterChangeValue)
    {
        hud.ResourceUI.SetItem(whatChanged, afterChangeValue);
    }

    public override void OnStateExit()
    {
        playerObserver.OnPlayerBackpackChanged -= PlayerObserver_OnPlayerBackpackChanged;
    }
}
