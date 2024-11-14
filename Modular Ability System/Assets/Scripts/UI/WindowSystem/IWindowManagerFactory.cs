using UI.MVP;
using UnityEngine;

namespace UI.WindowSystem
{
    public interface IWindowManagerFactory
    {
        TPresenter CreateWindowPresenter<TPresenter>(IView view, IModel windowModel)
            where TPresenter : class, IPresenter<IView, IModel>;
        TView CreateWindowView<TView>(Transform parent = null) where TView : Component, IView;
        TModel CreateWindowModel<TModel>() where TModel : class, IModel;
    }
}