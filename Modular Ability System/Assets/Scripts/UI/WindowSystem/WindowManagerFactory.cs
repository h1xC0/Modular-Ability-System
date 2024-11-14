using Core.Services.ResourceProvider;
using Shared.Factories;
using UI.MVP;
using UnityEngine;
using Zenject;

namespace UI.WindowSystem
{
    public class WindowManagerFactory : AbstractFactory, IWindowManagerFactory
    {
        private readonly IResourceProviderService _resourceProviderService;

        public WindowManagerFactory(DiContainer diContainer, IResourceProviderService resourceProviderService) : base(diContainer)
        {
            _resourceProviderService = resourceProviderService;
        }

        public TPresenter CreateWindowPresenter<TPresenter>(IView view, IModel windowModel)
            where TPresenter : class, IPresenter<IView, IModel>
        {
            return DiContainer.Instantiate<TPresenter>(new object[] { view, windowModel });
        }

        public TView CreateWindowView<TView>(Transform parent = null) where TView : Component, IView
        {
            var resource = _resourceProviderService.LoadResource<TView>(true);
            return CreateObject<TView>(resource.gameObject, parent);
        }
        
        public TModel CreateWindowModel<TModel>() where TModel : class, IModel =>
            DiContainer.Instantiate<TModel>();
    }
}