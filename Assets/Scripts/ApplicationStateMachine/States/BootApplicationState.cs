using ServiceLocations;
using SLS;

public class BootApplicationState : ApplicationBaseState
{
    public BootApplicationState(ApplicationContext context) : base(context)
    {
    }

    public override void Dispose()
    {
        
    }

    public override void OnStateEnter()
    {
        var loadingAction = ServiceLocator.Context.GetSingle<SceneLevelManager>().GoToLoadingScene();
        Context.ApplicationStateMachine.EnterState<LoadingApplicationState>().SetupLoadingProcess(loadingAction);
    }


    public override void OnStateExit()
    {
        
    }
}
