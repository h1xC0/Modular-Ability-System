using System;

namespace Core.Systems.Binders
{
    public interface IBinder<T> : IDisposable where T : class
    {
        IBinding<T> Bind<TKey>();
        void Unbind<TKey>();
        IBinding<T> GetBinding<TKey>();
    }
}