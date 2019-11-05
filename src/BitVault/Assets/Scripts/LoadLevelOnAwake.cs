using UnityEngine;

public sealed class LoadLevelOnAwake : MonoBehaviour
{
    [SerializeField] private GameState state;

    private void Awake()
    {
        Instantiate(state.SelectedLevel);
    }
}