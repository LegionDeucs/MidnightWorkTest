using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "ScriptableObjects/SceneData")]
public partial class SceneStaticData : ScriptableObject
{
    [SerializeField] private string _locationName;
    [SerializeField] private string _sceneName; 
    [SerializeField] private int _buildIndex;
    public string LocationName => _locationName;
    public string SceneName => _sceneName; 
    public int BuildIndex => _buildIndex;
}