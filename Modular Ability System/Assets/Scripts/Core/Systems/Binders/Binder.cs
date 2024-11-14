using System;
using System.Collections.Generic;
using Zenject;

namespace Core.Systems.Binders
{
    public class Binder<T> : IBinder<T> where T : class
    {
        protected readonly IInstantiator _instantiator;
        protected readonly Dictionary<Type, IBinding<T>> _bindings = new ();

        public Binder(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public IBinding<T> Bind<TKey>()
        {
            return Bind(typeof(TKey));
        }

        public void Unbind<TKey>()
        {
            Unbind(typeof(TKey));
        }

        public IBinding<T> GetBinding<TKey>()
        {
            return GetBinding(typeof(TKey));
        }

        protected virtual IBinding<T> Bind(Type key)
        {
            if (HasBinding(key))
            {
                throw new KeyNotFoundException($"The type {key} are already binded!");
            }

            var binding = InstallBinding(key);
            _bindings.Add(key, binding);
            return binding;
        }

        protected virtual void Unbind(Type key)
        {
            var binding = GetBinding(key);
            if (binding == null) return;

            _bindings.Remove(key);
            binding.Dispose();
        }

        protected virtual IBinding<T> GetBinding(Type key)
        {
            if (_bindings.ContainsKey(key) == false)
            {
                throw new KeyNotFoundException($"There is no binding installed for type {key}");
            }

            return _bindings[key];
        }

        protected virtual IBinding<T> InstallBinding(Type key)
        {
            return new Binding<T>(key, _instantiator);
        }

        protected bool HasBinding(Type key)
        {
            return _bindings.ContainsKey(key);
        }

        public void Dispose()
        {
            _bindings.Clear();
        }
    }
}