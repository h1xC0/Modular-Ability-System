using System;

namespace Shared.Extensions.Rx
{
    public class Actionable<T> : IActionable<T>
    {
        private event Action<T> _actionEvent;
        private event Action<Exception> _errorEvent;

        
        
        public Actionable(T source)
        {
            
        }
        
        public void Subscribe(Action<T> action)
        {
            _actionEvent += action;
        }

        public void OnNext(T value)
        {
            _actionEvent(value);
        }

        public void OnError(Exception error)
        {
            _errorEvent(error);
        }

        public void Dispose()
        {
            _actionEvent = null;
        }
    }
}