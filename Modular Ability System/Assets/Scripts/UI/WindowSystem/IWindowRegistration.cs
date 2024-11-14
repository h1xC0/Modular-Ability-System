using System;
using UI.MVP;
using UnityEngine;

namespace UI.WindowSystem
{
    public interface IWindowRegistration
    {
        void Register<TPresenter>(Action<Transform, Action<IView, IModel>> createMethod)
            where TPresenter : class, IPresenter<IView, IModel>;

        event Action<IPresenter<IView, IModel>> RegisterEvent;
    }
}