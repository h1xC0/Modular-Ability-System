using System;

namespace Shared.Extensions.Rx
{
    public interface IActionable<T> : IDisposable
    {
        void Subscribe(Action<T> action);
        void OnNext(T value);
        void OnError(Exception error);
    }
}