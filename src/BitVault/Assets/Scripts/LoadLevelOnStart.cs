using UnityEngine;

public sealed class LoadLevelOnAwake : MonoBehaviour
{
    [SerializeField] private GameState state;

    private void Start() => state.InitLevel();
}
