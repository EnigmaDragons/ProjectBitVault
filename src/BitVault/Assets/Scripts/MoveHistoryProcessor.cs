using UnityEngine;

public sealed class MoveHistoryProcessor : MonoBehaviour
{
    [SerializeField] private MoveHistory history;

    private void OnEnable()
    {
        Message.Subscribe<UndoRequested>(_ => history.Undo(), this);
        Message.Subscribe<LevelReset>(_ => history.Reset(), this);
        Message.Subscribe<PieceMoved>(p => history.Add(p), this);
        Message.Subscribe<StarCollected>(_ => history.Reset(), this); // TODO: Remove this once Star Undo is implemented
    }

    private void OnDisable()
    {
        Message.Unsubscribe(this);
    }
}
