using UnityEngine;

public class ApplicationStateMachine : StateMachine<ApplicationBaseState, ApplicationContext>
{
    public ApplicationStateMachine(ApplicationContext gameContext) : base()
    {
        states.Add(typeof(BootApplicationState), new BootApplicationState(gameContext));
        states.Add(typeof(EntryApplicationState), new EntryApplicationState(gameContext));
        states.Add(typeof(LoadingApplicationState), new LoadingApplicationState(gameContext));
        states.Add(typeof(GameApplicationState), new GameApplicationState(gameContext));

    }
}
