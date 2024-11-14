using Core.Systems.Binders;

namespace Core.Systems.CommandSystem
{
    public interface ICommandBinder: IBinder<ICommand>
    {
        ICommandBinding Bind<TSignal>() where TSignal : ISignal;
    }
}