using System;
using Zenject;

namespace Core.Systems.Binders
{
    public class Binding<T> : IBinding<T> where T : class
    {
        protected readonly Type _key;
        protected readonly IInstantiator _instantiator;
        protected Type _bindingType;
        
        public Binding(Type key, IInstantiator instantiator)
        {
            _key = key;
            _instantiator = instantiator;
        }

        protected T GetInstance(params object[] args)
        {
            return _instantiator.Instantiate(_bindingType, args) as T;
        }

        public void To<TKey>() where TKey : T
        {
            _bindingType = typeof(TKey);
        }

        public virtual void Dispose()
        {
            
        }
    }
}