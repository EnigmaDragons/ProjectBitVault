using UnityEngine;

public sealed class InitMapTokenizer : MonoBehaviour
{
    [SerializeField] private CurrentMapTokenizer map;
    [SerializeField] private GameLevel level;

    void Awake() => map.Init(level.Name);
}
