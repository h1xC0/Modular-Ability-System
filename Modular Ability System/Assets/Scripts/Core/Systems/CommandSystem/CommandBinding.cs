using System;
using System.Collections.Generic;
using Core.Systems.Binders;
using Zenject;

namespace Core.Systems.CommandSystem
{
    public class CommandBinding : Binding<ICommand>, ICommandBinding
    {
        private readonly IListener _commandListener;
        private List<Type> _commandsList = new ();

        private int _currentExecutedCommand = 0;
        private ICommand _currentCommand;
        private ICommandPayload _payload;

        public CommandBinding(Type key, IInstantiator instantiator, IListener listener) : base(key, instantiator)
        {
            _commandListener = listener;
            _commandListener.AddListener(key, Execute);
            _currentExecutedCommand = 0;
            _payload = null;
        }

        public ICommandBinding To<TCommand>(params object[] args) where TCommand : ICommand
        {
            _commandsList.Add(typeof(TCommand));
            return this;
        }
        
        private void Execute(ICommandPayload payload)
        {
            _payload = payload;
            
            if (_currentExecutedCommand >= _commandsList.Count)
            {
                _currentExecutedCommand = 0;
                return;
            }

            _currentCommand = _instantiator.Instantiate(_commandsList[_currentExecutedCommand]) as ICommand;
            if (_currentCommand == null) return;

            _currentCommand.OnExecuted += OnCommandExecuted;
            _currentCommand.Invoke(_payload);
        }

        private void OnCommandExecuted()
        {
            _currentCommand.OnExecuted -= OnCommandExecuted;
            _currentExecutedCommand += 1;
            Execute(_payload);
        }

        public override void Dispose()
        {
            _commandListener.RemoveListener(_key);
            base.Dispose();
        }
    }
}