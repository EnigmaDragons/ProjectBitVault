using UnityEngine;

public sealed class ResetLevelIfNotCompletedWhenRequested : MonoBehaviour
{
    [SerializeField] private GameState state;

    private bool _isCompleted;

    private void OnEnable()
    {
        Message.Subscribe<LevelResetRequested>(_ => Reset(), this);
        Message.Subscribe<LevelCompleted>(_ => _isCompleted = true, this);
    }

    private void OnDisable() => Message.Unsubscribe(this);

    private void Reset()
    {
        if (!_isCompleted)
            state.InitLevel();
    }
}
