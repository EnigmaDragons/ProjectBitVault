using UnityEngine;

public abstract class OnMessage<T> : MonoBehaviour
{
    private void OnEnable() => Message.Subscribe<T>(Execute, this);
    private void OnDisable() => Message.Unsubscribe(this);

    protected abstract void Execute(T msg);
}
