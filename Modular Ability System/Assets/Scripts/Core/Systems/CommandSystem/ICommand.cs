using System;

namespace Core.Systems.CommandSystem
{
    public interface ICommand : IDisposable
    {
        event Action OnExecuted;
        bool IsRetained { get; }
        void Invoke();
        void Invoke(ICommandPayload payload);
    }
}