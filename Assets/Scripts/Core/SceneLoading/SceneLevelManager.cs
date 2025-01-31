using System.Collections.Generic;
using UnityEngine;
using ServiceLocations;
using System;
using UnityEngine.SceneManagement;

public class SceneLevelManager : MonoBehaviour, IService
{
    [SerializeField] private SceneStaticData loadingScene;
    [SerializeField] private List<SceneStaticData> levels;

    public LoadingAction GoToLoadingScene() => LoadScene(loadingScene, LoadSceneMode.Single);
    public LoadingAction UnloadLoadingScreen() => UnloadScene(loadingScene);

    public LoadingAction GoToLevel(int levelNumber) => LoadScene(levels[(int)Mathf.Repeat(levelNumber - 1, levels.Count)], LoadSceneMode.Additive);

    protected LoadingAction LoadScene(SceneStaticData sceneStaticData, LoadSceneMode loadSceneMode)
    {
        LoadingAction loadingAction = new LoadingAction();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneStaticData.BuildIndex, loadSceneMode);
        operation.completed += Operation_completed;

        void Operation_completed(AsyncOperation obj)
        {
            operation.completed -= Operation_completed;
            loadingAction.CompleteOperation();
        }

        return loadingAction;
    }

    protected LoadingAction UnloadScene(SceneStaticData sceneStaticData)
    {
        LoadingAction loadingAction = new LoadingAction();

        AsyncOperation operation = SceneManager.UnloadSceneAsync(sceneStaticData.BuildIndex);
        operation.completed += Operation_completed;

        void Operation_completed(AsyncOperation obj)
        {
            operation.completed -= Operation_completed;
            loadingAction.CompleteOperation();
        }

        return loadingAction;
    }
}
public class LoadingAction
{
    public event System.Action OnComplete;

    public void CompleteOperation() => OnComplete?.Invoke();
}