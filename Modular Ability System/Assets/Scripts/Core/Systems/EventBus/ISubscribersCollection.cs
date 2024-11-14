using System;
using System.Collections.Generic;

namespace Core.Systems.EventBus
{
    public interface ISubscribersCollection<TSubscriber> : IDisposable where TSubscriber : class, ISubscriber
    {
        bool Executing { get; set; }
        void Add(TSubscriber subscriber);
        void Remove(TSubscriber subscriber);
        void Cleanup();
        IEnumerable<TSubscriber> GetCollection();
    }
}