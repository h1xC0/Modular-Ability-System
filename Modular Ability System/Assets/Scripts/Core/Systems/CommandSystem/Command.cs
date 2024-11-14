using System;

namespace Core.Systems.CommandSystem
{
    public class Command : ICommand
    {
        public event Action OnExecuted;
        public bool IsRetained { get; private set; }
        
        public void Invoke()
        {
            Execute();
        }

        public void Invoke(ICommandPayload payload)
        {
            if (payload == null)
            {
                Execute();
            }
            else
            {
                Execute(payload);
            }
        }
        
        protected void Retain()
        {
            IsRetained = true;
        }

        protected void Release()
        {
            IsRetained = false;
            OnExecuted?.Invoke();
        }

        protected virtual void Execute()
        {
            
        }
        
        protected virtual void Execute(ICommandPayload payload)
        {
            
        }
        
        public void Dispose()
        {
        }
    }
}
