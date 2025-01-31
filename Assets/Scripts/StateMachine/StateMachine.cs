using System;
using System.Collections.Generic;

public abstract class StateMachine<TBaseState, TContext> where TBaseState : BaseState<TContext> where TContext : IStateMachineContext, new()
{
    protected Dictionary<Type, TBaseState> states;

    protected TBaseState currentState;

    public StateMachine()
    {
        states = new Dictionary<Type, TBaseState>();
    }

    public TState EnterState<TState>() where TState : BaseState<TContext>
    {
        currentState?.OnStateExit();
        currentState = null;

        Type type = typeof(TState);

        if (states.ContainsKey(type))
            currentState = states[type];

        currentState?.OnStateEnter();
        return currentState as TState;
    }
}
