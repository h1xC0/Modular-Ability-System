using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared.Extensions.Rx
{
    [Serializable]
    public class ReactiveCollection<T> : IReactiveCollection<T>
    {
        public IEnumerable<T> Collection => _collection;
        private IEnumerable<T> _collection;

        public int Count => _collection.Count(); 
        
        private event Action<T> CollectionAddEvent;
        private event Action<T> CollectionRemoveEvent;
        private event Action<int> CollectionChangeEvent;

        private bool _shouldInvokeOnAdd;
        private bool _shouldInvokeOnRemove;
        private bool _shouldInvokeOnChange;
        
        public ReactiveCollection(IEnumerable<T> collection = default)
        {
            _collection = collection ?? new List<T>();
            _shouldInvokeOnAdd = false;
            _shouldInvokeOnRemove = false;
        }

        public void Add(T value)
        {
            var list = _collection.ToList();
            list.Add(value);
            _collection = list;
            
            NotifyOnAdd(value);
        }

        public void AddRange(IEnumerable<T> enumerable)
        {
            var list = _collection.ToList();
            var newList = enumerable.ToList();
            
            list.AddRange(newList);
            _collection = list;

            foreach (var value in newList)
            {
                NotifyOnAdd(value);
            }
        }

        public void Remove(T value)
        {
            var list = _collection.ToList();
            list.Remove(value);
            _collection = list;
            
            NotifyOnRemove(value);
        }

        public void Clear()
        {
            var list = _collection.ToList();
            list.Clear();
            _collection = list;
        }
        
        public ICollectionItemSubscriptable<T> ObserveAdd()
        {
            _shouldInvokeOnAdd = true;
            return this;
        }

        public ICollectionItemSubscriptable<T> ObserveRemove()
        {
            _shouldInvokeOnRemove = true;
            return this;
        }

        public ICollectionChangeSubscriptable ObserveCountChanged()
        {
            _shouldInvokeOnChange = true;
            return this;
        }

        public void ForceAddValue(T value)
        {
            _collection.ToList().Add(value);
        }

        public IDisposable Subscribe(Action<T> listener)
        {
            if (_shouldInvokeOnAdd)
            {
                CollectionAddEvent += listener;
            }

            if (_shouldInvokeOnRemove)
            {
                CollectionRemoveEvent += listener;
            }
            
            return this;
        }

        public IDisposable Subscribe(Action<int> listener)
        {
            if (_shouldInvokeOnChange)
            {
                CollectionChangeEvent += listener;
            }

            return this;
        }

        private void NotifyOnAdd(T value)
        {
            CollectionAddEvent?.Invoke(value);
        }

        private void NotifyOnRemove(T value)
        {
            CollectionRemoveEvent?.Invoke(value);
        }
        
        public void Dispose()
        {
            if (CollectionAddEvent == null || CollectionRemoveEvent == null) return;
            
            foreach (var d in CollectionAddEvent.GetInvocationList())
                CollectionAddEvent -= (d as Action<T>);
            
            foreach (var d in CollectionRemoveEvent.GetInvocationList())
                CollectionRemoveEvent -= (d as Action<T>);
        }    
    }

    public static class ReactiveCollectionExtensions
    {
        public static ReactiveCollection<T> ToReactiveCollection<T>(this IEnumerable<T> source)
        {
            return new ReactiveCollection<T>(source);
        }
    }
}