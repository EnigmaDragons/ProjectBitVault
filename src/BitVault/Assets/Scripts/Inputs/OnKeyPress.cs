using UnityEngine;
using UnityEngine.Events;

public sealed class OnKeyPress : MonoBehaviour
{
    [SerializeField] private KeyCode key;
    [SerializeField] private UnityEvent action;

    private bool _inputBlocked;

    private void OnEnable()
    {
        _inputBlocked = false;
        Message.Subscribe<PieceMoved>(msg => _inputBlocked = true, this);
        Message.Subscribe<PieceMoveFinished>(msg => _inputBlocked = false, this);
    }

    private void OnDisable() => Message.Unsubscribe(this);

    private void Update()
    {
        if (Input.GetKeyDown(key) && !_inputBlocked)
            action.Invoke();
    }
}
