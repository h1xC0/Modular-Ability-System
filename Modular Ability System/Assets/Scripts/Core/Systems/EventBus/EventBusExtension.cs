using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Systems.EventBus
{
    public class EventBusExtension : IDisposable
    {
        private Dictionary<Type, List<Type>> _cachedSubscriberTypes;

        public EventBusExtension()
        {
            _cachedSubscriberTypes = new Dictionary<Type, List<Type>>();
        }
        public List<Type> GetSubscriberTypes(
            ISubscriber subscriber)
        {
            
            Type type = subscriber.GetType();
            if (_cachedSubscriberTypes.ContainsKey(type))
            {
                return _cachedSubscriberTypes[type];
            }

            var subscriberTypes = type
                .GetInterfaces()
                .Where(t => t.GetInterfaces()
                    .Contains(typeof(ISubscriber)))
                .ToList();

            _cachedSubscriberTypes[type] = subscriberTypes;
            return subscriberTypes;
        }

        public void Dispose()
        {
            _cachedSubscriberTypes.Clear();
        }
    }
}