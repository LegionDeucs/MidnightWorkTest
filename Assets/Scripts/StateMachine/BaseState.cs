using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseState<IContext> : IDisposable where IContext : IStateMachineContext, new()
{
    protected IContext Context { get; private set; }

    public BaseState(IContext context)
    {
        Context = context;
    }

    public abstract void OnStateEnter();
    public abstract void OnStateExit();
    public abstract void Dispose();
}
