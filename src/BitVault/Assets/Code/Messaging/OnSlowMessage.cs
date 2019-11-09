using System.Collections.Generic;
using UnityEngine;

public abstract class OnSlowMessage<T> : MonoBehaviour
{
    private readonly List<MessageTask> _tasks = new List<MessageTask>(); 

    private void OnEnable() => Message.Subscribe<T>(BeginExecution, this);
    private void OnDisable() => Message.Unsubscribe(this);
    private void OnDestroy() => _tasks.ForEach(x => x.Done(this));

    private void BeginExecution(T msg, MessageTask task)
    {
        _tasks.Add(task);
        Execute(msg, task);
    }

    protected abstract void Execute(T msg, MessageTask task);
}
