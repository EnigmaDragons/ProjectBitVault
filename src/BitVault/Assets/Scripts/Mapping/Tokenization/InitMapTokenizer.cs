using UnityEngine;

public sealed class InitMapTokenizer : MonoBehaviour
{
    [SerializeField] private CurrentMapTokenizer map;

    void Awake() => map.Init();
}
