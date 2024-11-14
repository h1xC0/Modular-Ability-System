using System;

namespace Core.Systems.CommandSystem
{
    public interface IListener
    {
        void AddListener(Type type, Action<ICommandPayload> action);
        void RemoveListener(Type type);
    }
}