using System;
using Core.Services.ViewLayerService;
using UI.MVP;

namespace UI.WindowSystem
{
    public interface IWindowManager
    {
        event Action<IPresenter<IView, IModel>> WindowOpenEvent;
        event Action<IPresenter<IView, IModel>> WindowCloseEvent;
        event Action<IPresenter<IView, IModel>> OnWindowFocusEvent;
        event Action<IPresenter<IView, IModel>> OnWindowUnfocusedEvent;
        event Action<ViewLayer> ClearedLayerEvent;

        TPresenter Open<TPresenter>(ViewState state = ViewState.None)
            where TPresenter : class, IPresenter<IView, IModel>;

        bool IsOpened<TPresenter>() 
            where TPresenter : class, IPresenter<IView, IModel>;

        void Close(IPresenter<IView, IModel> windowPresenter, bool executeImmediately = false);
        void ClearLayer(ViewLayer viewLayer);
        void ClearLayer(string layerName);
        IWindowOpenInfo GetActiveWindow(ViewLayer layer);
        IWindowOpenInfo GetActiveWindow(string layerName);
        IWindowOpenInfo GetPriorityWindow();
        IWindowOpenInfo GetPriorityWindow(string layerName);
    }
}