using System;

namespace Shared.Extensions.Rx
{
    public interface IReactiveProperty<T> : IDisposable
    {
        public bool HasValue { get; }
        T Value { get; set; }
        void ForceSetValue(T value);
        IDisposable Subscribe(Action<T> listener);
    }
}