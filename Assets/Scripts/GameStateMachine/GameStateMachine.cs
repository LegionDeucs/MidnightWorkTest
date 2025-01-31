using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : StateMachine<GameBaseState, GameContext>
{

    public GameStateMachine(GameContext gameContext) : base()
    {
        states.Add(typeof(BootGameState), new BootGameState(gameContext));
        states.Add(typeof(InWorldState), new InWorldState(gameContext));
        states.Add(typeof(MenuGameState), new MenuGameState(gameContext));
    }

}