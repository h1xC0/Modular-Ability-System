using System;

namespace Shared.Extensions.Rx
{
    public interface ICollectionItemSubscriptable<out T> : IDisposable
    {
        IDisposable Subscribe(Action<T> listener);
    }

    public interface ICollectionChangeSubscriptable : IDisposable
    {
        IDisposable Subscribe(Action<int> listener);
    }
}