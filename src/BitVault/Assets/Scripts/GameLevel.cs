using UnityEngine;

[CreateAssetMenu]
public class GameLevel : ScriptableObject
{
    [SerializeField] private string levelName;
    [SerializeField] private GameObject prefab;
    [SerializeField] private bool isTutorial = false;

    public string Name => levelName;
    public GameObject Prefab => prefab;
    public bool IsTutorial => isTutorial;
}
