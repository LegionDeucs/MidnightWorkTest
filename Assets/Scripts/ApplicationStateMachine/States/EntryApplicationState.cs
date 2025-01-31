using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryApplicationState : ApplicationBaseState
{
    public EntryApplicationState(ApplicationContext context) : base(context)
    {
    }

    public override void Dispose()
    {
        
    }

    public override void OnStateEnter()
    {
        Context.ApplicationStateMachine.EnterState<BootApplicationState>();
    }

    public override void OnStateExit()
    {
        
    }
}
