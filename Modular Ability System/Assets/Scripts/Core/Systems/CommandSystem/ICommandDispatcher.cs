using System;

namespace Core.Systems.CommandSystem
{
    public interface ICommandDispatcher : IDisposable
    {
        void Dispatch<TSignal>() where TSignal : ISignal;
        void Dispatch<TSignal>(ICommandPayload payload) where TSignal : ISignal;
        bool HasListener(Type type);
    }
}