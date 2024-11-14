using System;
using System.Collections.Generic;

namespace Core.Systems.CommandSystem
{
    public class CommandDispatcher : ICommandDispatcher, IListener
    {
        private readonly Dictionary<Type, Action<ICommandPayload>> _signals = new ();

        public void Dispatch<TSignal>() where TSignal : ISignal
        {
            Dispatch<TSignal>(null);
        }

        public void Dispatch<TSignal>(ICommandPayload payload) where TSignal : ISignal
        {
            var key = typeof(TSignal);
            if (HasListener(key) == false)
            {
                throw new KeyNotFoundException($"Signal {key} wasn't binded!");
            }
            
            _signals[key].Invoke(payload);
        }
        
        public void AddListener(Type type, Action<ICommandPayload> action)
        {
            if (HasListener(type))
            {
                throw new KeyNotFoundException($"Signal {type} already binded!");
            }
            
            _signals.Add(type, action);
        }

        public void RemoveListener(Type type)
        {
            if (HasListener(type))
            {
                _signals.Remove(type);
            }
        }

        public bool HasListener(Type type)
        {
            return _signals.ContainsKey(type);
        }
        
        public void Dispose()
        {
            _signals.Clear();
        }
    }
}