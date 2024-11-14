using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Systems.EventBus
{
    public class EventBusService : IEventBusService
    {
        private readonly Dictionary<Type, ISubscribersCollection<ISubscriber>> _subscribers;

        private EventBusExtension _eventBusExtension;

        public EventBusService()
        {
            _subscribers = new Dictionary<Type, ISubscribersCollection<ISubscriber>>();
            _eventBusExtension = new EventBusExtension();
        }
        
        public void Subscribe(ISubscriber subscriber)
        {
            var subscriberTypes = _eventBusExtension.GetSubscriberTypes(subscriber);
            foreach (Type subscriberType in subscriberTypes)
            {
                
                if (_subscribers.ContainsKey(subscriberType) == false)
                {
                    _subscribers[subscriberType] = new SubscribersCollection<ISubscriber>();
                }
                _subscribers[subscriberType].Add(subscriber);
            }
        }
        
        public void Unsubscribe(ISubscriber subscriber)
        {
            List<Type> subscriberTypes = _eventBusExtension.GetSubscriberTypes(subscriber);
            foreach (Type subscriberType in subscriberTypes)
            {
                if (_subscribers.TryGetValue(subscriberType, out var foundSubscriber))
                    foundSubscriber.Remove(subscriber);
            }
        }
        
        public void RaiseEvent<TSubscriber>(Action<TSubscriber> action)
            where TSubscriber : class, ISubscriber
        {
            ISubscribersCollection<ISubscriber> subscribers = _subscribers[typeof(TSubscriber)];
	
            subscribers.Executing = true;
            foreach (ISubscriber subscriber in subscribers.GetCollection())
            {
                try
                {
                    action.Invoke(subscriber as TSubscriber);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            subscribers.Executing = false;
            subscribers.Cleanup();
        }

        public void Dispose()
        {
            _subscribers.Clear();
        }
    }
}