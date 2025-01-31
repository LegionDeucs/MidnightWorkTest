using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBaseState : BaseState<GameContext>
{
    protected GameBaseState(GameContext context) : base(context)
    {
    }
}
