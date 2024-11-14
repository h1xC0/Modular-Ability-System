using System;
using UnityEngine;

namespace Shared.Extensions.Rx
{
    [Serializable]
    public class ReactiveProperty<T> : IReactiveProperty<T>
    {
        public bool HasValue => _value != null;
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                NotifySubscribers(value);
            }
        }

        [SerializeField] private T _value;

        public ReactiveProperty(T value = default)
        {
            _value = value;
        }

        private event Action<T> ValueEvent;

        public void ForceSetValue(T value)
        {
            _value = value;
        }

        public IDisposable Subscribe(Action<T> listener)
        {
            ValueEvent += listener;
            return this;
        }

        private void NotifySubscribers(T value)
        {
            ValueEvent?.Invoke(value);
        }
        
        public void Dispose()
        {
            if (ValueEvent == null) return;
            
            foreach (var d in ValueEvent.GetInvocationList())
                ValueEvent -= (d as Action<T>);
        }
    }
    
    public static class ReactivePropertyExtensions
    {
        public static ReactiveProperty<T> ToReactiveProperty<T>(this T source)
        {
            return new ReactiveProperty<T>(source);
        }
    }
}