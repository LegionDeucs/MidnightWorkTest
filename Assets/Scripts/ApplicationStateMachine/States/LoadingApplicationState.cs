using ServiceLocations;
using SLS;

public class LoadingApplicationState : ApplicationBaseState
{
    private LoadingAction currentLoadingAction;

    private SaveLoadSystem saveLoadSystem;
    private SceneLevelManager sceneLevelManager;

    public LoadingApplicationState(ApplicationContext context) : base(context)
    {
        saveLoadSystem = ServiceLocator.Context.GetSingle<SaveLoadSystem>();
        sceneLevelManager = ServiceLocator.Context.GetSingle<SceneLevelManager>();
    }
    internal void SetupLoadingProcess(LoadingAction loadingAction)
    {
        Dispose();
        saveLoadSystem.Init();

        currentLoadingAction = loadingAction;
        currentLoadingAction.OnComplete += OnLoadingSceneLoadingComplete;
    }

    private void OnLoadingSceneLoadingComplete()
    {
        currentLoadingAction.OnComplete -= OnLoadingSceneLoadingComplete;
        
        currentLoadingAction = sceneLevelManager.GoToLevel(Context.CurrentLevel);

        currentLoadingAction.OnComplete += OnLevelLoaded;
    }

    private void OnLevelLoaded()
    {
        currentLoadingAction = sceneLevelManager.UnloadLoadingScreen();
        currentLoadingAction.OnComplete += CurrentLoadingAction_OnComplete;
    }

    private void CurrentLoadingAction_OnComplete()
    {
        currentLoadingAction.OnComplete -= CurrentLoadingAction_OnComplete;
        Context.ApplicationStateMachine.EnterState<GameApplicationState>();
    }

    public override void Dispose()
    {
        if (currentLoadingAction != null)
        {
            currentLoadingAction.OnComplete -= OnLoadingSceneLoadingComplete;
            currentLoadingAction.OnComplete -= OnLevelLoaded;
        }
         
        currentLoadingAction = null;
    }

    public override void OnStateEnter()
    {
        
    }

    public override void OnStateExit()
    {
        Dispose();
    }
}
