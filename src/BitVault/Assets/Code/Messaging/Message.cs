﻿using System.Collections.Generic;
using System;
using System.Linq;

public static class Message 
{
    private static readonly List<MessageSubscription> EventSubs = new List<MessageSubscription>();
    private static readonly Messages Msgs = new Messages();

    public static int SubscriptionCount => Msgs.SubscriptionCount;
    public static void Publish(object payload) => Msgs.Publish(payload);
    public static void Subscribe<T>(Action<T> onEvent, object owner) => Subscribe(MessageSubscription.Create(onEvent, owner));

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
        private readonly Dictionary<Type, List<object>> _eventActions = new Dictionary<Type, List<object>>();
        private readonly Dictionary<object, List<MessageSubscription>> _ownerSubscriptions = new Dictionary<object, List<MessageSubscription>>();

        private List<object> _eventQueue = new List<object>();
        private bool _isPublishing;

        public int SubscriptionCount => _eventActions.Sum(e => e.Value.Count);

        public void Publish(object payload)
        {
            _eventQueue.Add(payload);
            PublishLoop();
        }

        public void Subscribe(MessageSubscription subscription)
        {
            var eventType = subscription.EventType;
            if (!_eventActions.ContainsKey(eventType))
                _eventActions[eventType] = new List<object>();
            if (!_ownerSubscriptions.ContainsKey(subscription.Owner))
                _ownerSubscriptions[subscription.Owner] = new List<MessageSubscription>();
            _eventActions[eventType].Add(subscription.OnEvent);
            _ownerSubscriptions[subscription.Owner].Add(subscription);
        }

        public void Unsubscribe(object owner)
        {
            if (!_ownerSubscriptions.ContainsKey(owner))
                return;
            var events = _ownerSubscriptions[owner];
            for (var i = 0; i < _eventActions.Count; i++)
                _eventActions.ElementAt(i).Value.RemoveAll(x => events.Any(y => y.OnEvent.Equals(x)));
            _ownerSubscriptions.Remove(owner);
        }

        private void PublishLoop()
        {
            if (_isPublishing)
                return;
            _isPublishing = true;
            while (_eventQueue.Any())
            {
                var nextEvent = _eventQueue[0];
                _eventQueue = _eventQueue.Where(x => x != nextEvent).ToList();
                InstantPublish(nextEvent);
            }
            _isPublishing = false;
        }

        private void InstantPublish(object payload)
        {
            var eventType = payload.GetType();

            if (_eventActions.ContainsKey(eventType))
                foreach (var action in _eventActions[eventType].ToList())
                    ((Action<object>)action)(payload);
        }
    }
}
