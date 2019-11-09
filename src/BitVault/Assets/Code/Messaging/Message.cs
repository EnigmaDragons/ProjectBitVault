using System.Collections.Generic;
using System;
using System.Linq;

public static class Message 
{
    private static readonly List<MessageSubscription> EventSubs = new List<MessageSubscription>();
    private static readonly Messages Msgs = new Messages();

    public static int SubscriptionCount => Msgs.SubscriptionCount;
    public static void Publish(object payload) => Msgs.Publish(payload, new Maybe<object>());
    public static void Publish(object payload, object completedPayload) => Msgs.Publish(payload, new Maybe<object>(completedPayload));
    public static void Subscribe<T>(Action<T> onEvent, object owner) => Subscribe(MessageSubscription.Create(onEvent, owner));
    public static void Subscribe<T>(Action<T, MessageTask> onEvent, object owner) => Subscribe(MessageSubscription.Create(onEvent, owner));

    private static void Subscribe(MessageSubscription subscription)
    {
        Msgs.Subscribe(subscription);
        EventSubs.Add(subscription);
    }

    public static void Unsubscribe(object owner)
    {
        Msgs.Unsubscribe(owner);
        EventSubs.Where(x => x.Owner.Equals(owner)).ForEach(x =>
        {
            EventSubs.Remove(x);
        });
    }
    
    private sealed class Messages
    {
        private readonly Dictionary<Type, List<MessageSubscription>> _eventActions = new Dictionary<Type, List<MessageSubscription>>();
        private readonly Dictionary<object, List<MessageSubscription>> _ownerSubscriptions = new Dictionary<object, List<MessageSubscription>>();

        public int SubscriptionCount => _eventActions.Sum(e => e.Value.Count);

        public void Publish(object payload, Maybe<object> completedPayload)
        {
            var eventType = payload.GetType();

            if (_eventActions.ContainsKey(eventType))
            {
                var task = new MessageTask(completedPayload, _eventActions[eventType].Select(x => x.Owner).ToList());
                foreach (var sub in _eventActions[eventType].ToList())
                    sub.OnEvent(payload, task);
            }
        }

        public void Subscribe(MessageSubscription subscription)
        {
            var eventType = subscription.EventType;
            if (!_eventActions.ContainsKey(eventType))
                _eventActions[eventType] = new List<MessageSubscription>();
            if (!_ownerSubscriptions.ContainsKey(subscription.Owner))
                _ownerSubscriptions[subscription.Owner] = new List<MessageSubscription>();
            _eventActions[eventType].Add(subscription);
            _ownerSubscriptions[subscription.Owner].Add(subscription);
        }

        public void Unsubscribe(object owner)
        {
            if (!_ownerSubscriptions.ContainsKey(owner))
                return;
            var events = _ownerSubscriptions[owner];
            for (var i = 0; i < _eventActions.Count; i++)
                _eventActions.ElementAt(i).Value.RemoveAll(x => events.Any(y => y.Equals(x)));
            _ownerSubscriptions.Remove(owner);
        }
    }
}
