using UnityEngine;

[CreateAssetMenu]
public class GameLevel : ScriptableObject
{
    [SerializeField] private string levelName;
    [SerializeField] private GameObject prefab;

    public string Name => levelName;
    public GameObject Prefab => prefab;
}
