using System;

namespace Core.Systems.EventBus
{
    public interface IEventBusService : IDisposable
    {
        void Subscribe(ISubscriber subscriber);
        void Unsubscribe(ISubscriber subscriber);
        void RaiseEvent<TSubscriber>(Action<TSubscriber> action)
            where TSubscriber : class, ISubscriber;
    }
}