using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ApplicationBaseState : BaseState<ApplicationContext>
{
    public ApplicationBaseState(ApplicationContext context) : base(context)
    {
    }
}
