using System;

public sealed class MessageSubscription
{
    public Type EventType { get; }
    public Action<object, MessageTask> OnEvent { get; }
    public object Owner { get; }

    internal MessageSubscription(Type eventType, Action<object> onEvent, object owner)
        : this(eventType, (o, task) =>
        {
            onEvent(o);
            task.Done(owner);
        }, owner) {}

    internal MessageSubscription(Type eventType, Action<object, MessageTask> onEvent, object owner)
    {
        EventType = eventType;
        OnEvent = onEvent;
        Owner = owner;
    }

    public static MessageSubscription Create<T>(Action<T> onEvent, object owner) 
        => new MessageSubscription(typeof(T), (o) => { if (o.GetType() == typeof(T)) onEvent((T)o); }, owner);

    public static MessageSubscription Create<T>(Action<T, MessageTask> onEventWithTask, object owner)
        => new MessageSubscription(typeof(T), (o, task) => { if (o.GetType() == typeof(T)) onEventWithTask((T)o, task); }, owner);
}
