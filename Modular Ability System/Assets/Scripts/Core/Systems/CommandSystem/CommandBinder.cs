using System;
using Core.Systems.Binders;
using Zenject;

namespace Core.Systems.CommandSystem
{
    public class CommandBinder : Binder<ICommand>, ICommandBinder
    {
        private readonly IListener _commandListener;
        
        public CommandBinder(IInstantiator instantiator, IListener listener) 
            : base(instantiator)
        {
            _commandListener = listener;
        }

        protected override IBinding<ICommand> InstallBinding(Type key)
        {
            return new CommandBinding(key, _instantiator, _commandListener);
        }

        public new ICommandBinding Bind<TSignal>() where TSignal : ISignal
        {
            return base.Bind<TSignal>() as ICommandBinding;
        }
    }
}