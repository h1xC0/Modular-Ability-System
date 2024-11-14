using System.Collections.Generic;

namespace Shared.Extensions.Rx
{
    public interface IReactiveCollection<T> : ICollectionItemSubscriptable<T>, ICollectionChangeSubscriptable
    {
        IEnumerable<T> Collection { get; }
        int Count { get; }
        void Add(T value);
        void AddRange(IEnumerable<T> enumerable);
        void Remove(T value);
        ICollectionItemSubscriptable<T> ObserveAdd();
        ICollectionItemSubscriptable<T> ObserveRemove();
        ICollectionChangeSubscriptable ObserveCountChanged();
        void ForceAddValue(T value);
    }
}