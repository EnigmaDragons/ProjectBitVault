using UnityEngine;

[CreateAssetMenu]
public class GameLevel : ScriptableObject
{
    [SerializeField] private string levelName;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Solution solution;

    public string Name => levelName;
    public GameObject Prefab => prefab;
    public Maybe<Solution> Solution => new Maybe<Solution>(solution);
}
