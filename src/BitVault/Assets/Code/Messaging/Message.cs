using System.Collections.Generic;
using System;
using System.Linq;

public static class Message 
{
    private static readonly List<MessageSubscription> EventSubs = new List<MessageSubscription>();
    private static readonly Messages Messages = new Messages();

    public static int SubscriptionCount => Messages.SubscriptionCount;
    public static void Publish(object payload) => Messages.Publish(payload);
    public static void Subscribe<T>(Action<T> onEvent, object owner) => Subscribe(MessageSubscription.Create(onEvent, owner));

    private static void Subscribe(MessageSubscription subscription)
    {
        Messages.Subscribe(subscription);
        EventSubs.Add(subscription);
    }

    public static void Unsubscribe(object owner)
    {
        Messages.Unsubscribe(owner);
        EventSubs.Where(x => x.Owner.Equals(owner)).ForEach(x =>
        {
            EventSubs.Remove(x);
        });
    }
}
