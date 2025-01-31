using ServiceLocations;
using SLS;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ApplicationContext : MonoBehaviour, IStateMachineContext 
{
    [SerializeField] private SceneLevelManager sceneLevelManager;
    [SerializeField] private ItemsEncyclopedia itemsEncyclopedia;
    [SerializeField] private DataID levelDataID;
    public ApplicationStateMachine ApplicationStateMachine { get; private set; }
    public SceneLevelManager LevelManager => sceneLevelManager;

    private SaveLoadSystem saveLoadSystem;

    public int CurrentLevel => ServiceLocator.Context.GetSingle<SaveLoadSystem>().saveLoadSystemCache.level;

    private void Awake()
    {
        saveLoadSystem = new SaveLoadSystem();
        DontDestroyOnLoad(gameObject);
        SetupServices();
        ApplicationStateMachine = new ApplicationStateMachine(this);
        ApplicationStateMachine.EnterState<BootApplicationState>();

        Debug.Log("GameContextInited");
    }

    private void SetupServices()
    {
        ServiceLocator serviceLocator = new();
        serviceLocator.BindSingle(sceneLevelManager);
        serviceLocator.BindSingle(saveLoadSystem);
        serviceLocator.BindSingle(itemsEncyclopedia);
    }

    private void OnApplicationFocus(bool focus)
    {
        if(saveLoadSystem != null)
            saveLoadSystem.Save();
    }
}
