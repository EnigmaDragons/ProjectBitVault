using UnityEngine;

[CreateAssetMenu]
public class GameLevel : ScriptableObject
{
    [SerializeField] private string levelName;
    [SerializeField] private GameObject prefab;
    [SerializeField] private StarObjective[] starObjectives;

    public string Name => levelName;
    public GameObject Prefab => prefab;
    public StarObjective[] StarObjectives => starObjectives;
}
