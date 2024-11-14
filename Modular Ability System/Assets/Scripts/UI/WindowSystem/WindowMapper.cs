using UI.MVP;
using UnityEngine;

namespace UI.WindowSystem
{
    public class WindowMapper<TPresenter, TView, TModel>
        where TPresenter : class, IPresenter<IView, IModel>
        where TView : Component, IView
        where TModel : class, IModel
    {
        private readonly IWindowRegistration _windowRegister;
        private readonly IWindowManagerFactory _windowFactory;

        protected WindowMapper(IWindowRegistration windowRegister,
            IWindowManagerFactory windowFactory)
        {
            _windowRegister = windowRegister;
            _windowFactory = windowFactory;
            
            Register();
        }

        private void Register()
        {
            _windowRegister.Register<TPresenter>((parent, action) =>
            {
                action.Invoke(_windowFactory.CreateWindowView<TView>(parent),
                    _windowFactory.CreateWindowModel<TModel>());
            });
        }
    }
}