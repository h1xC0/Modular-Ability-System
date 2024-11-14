using Core.Systems.Binders;

namespace Core.Systems.CommandSystem
{
    public interface ICommandBinding: IBinding<ICommand>
    {
        ICommandBinding To<TCommand>(params object[] args) where TCommand : ICommand;
    }
}