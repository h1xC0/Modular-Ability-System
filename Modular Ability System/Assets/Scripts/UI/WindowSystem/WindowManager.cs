using System;
using System.Collections.Generic;
using System.Linq;
using Core.Services.ViewLayerService;
using UI.MVP;
using UnityEngine;

namespace UI.WindowSystem
{
    public class WindowManager : IWindowManager, IWindowRegistration
    {
        private readonly IWindowManagerFactory _windowManagerFactory;
        private readonly IViewLayerService _viewLayerService;
        private readonly Dictionary<IPresenter<IView, IModel>, IWindowOpenInfo> _windowsOpenData;
        
        private readonly Dictionary<Type, Action<Transform, Action<IView, IModel>>> _viewModelCreators;
        private readonly Dictionary<Type, ViewLayer> _presentersLayers;
        private readonly Dictionary<ViewLayer, IWindowOpenInfo> _lastActiveWindows;
        private readonly int _indexOfLastEscapableLayer = 3;

        public event Action<IPresenter<IView, IModel>> WindowOpenEvent;
        public event Action<IPresenter<IView, IModel>> WindowCloseEvent;
        public event Action<IPresenter<IView, IModel>> RegisterEvent; 
        public event Action<IPresenter<IView, IModel>> OnWindowFocusEvent;
        public event Action<IPresenter<IView, IModel>> OnWindowUnfocusedEvent; 
        public event Action<ViewLayer> ClearedLayerEvent;

        public WindowManager(IWindowManagerFactory windowManagerFactory, IViewLayerService viewLayerService)
        {
            _windowManagerFactory = windowManagerFactory;
            _viewLayerService = viewLayerService;

            _viewModelCreators = new Dictionary<Type, Action<Transform, Action<IView, IModel>>>();
            _presentersLayers = new Dictionary<Type, ViewLayer>();
            _lastActiveWindows = new Dictionary<ViewLayer, IWindowOpenInfo>();
            _windowsOpenData = new Dictionary<IPresenter<IView, IModel>, IWindowOpenInfo>();
            
            _viewLayerService.SetupWindowLayers();

            foreach (var layer in _viewLayerService.Layers)
            {
                _lastActiveWindows.Add(layer, null);
            }
        }
        
        public TPresenter Open<TPresenter>(ViewState state = ViewState.None)
            where TPresenter : class, IPresenter<IView, IModel>
        {
            if (IsOpened<TPresenter>())
                return null;
            
            _presentersLayers.TryGetValue(typeof(TPresenter), out ViewLayer layer);

            if (layer != null)
            {
                IWindowOpenInfo windowOpenInfo = CreateWindow<TPresenter>(state);
            
                var lastActiveWindow = _lastActiveWindows[windowOpenInfo.ViewLayer];
                if (lastActiveWindow == null)
                {
                    lastActiveWindow = windowOpenInfo;
                }
                else if (windowOpenInfo != _lastActiveWindows[windowOpenInfo.ViewLayer])
                {
                    lastActiveWindow = windowOpenInfo;
                }
                Debug.Log($"Try Open<{typeof(TPresenter)}> with Layer {windowOpenInfo.ViewLayer}");
                
                OnWindowUnfocusedEvent?.Invoke(GetPriorityWindow(LayerName.Default)?.Presenter);
                WindowOpenEvent?.Invoke(windowOpenInfo.Presenter);
                windowOpenInfo.Presenter.Open();
            
                if (lastActiveWindow.Presenter.Equals(windowOpenInfo.Presenter))
                {
                    OnWindowFocusEvent?.Invoke(windowOpenInfo.Presenter);
                    _lastActiveWindows[windowOpenInfo.ViewLayer] = lastActiveWindow;
                }
                
                return (TPresenter)windowOpenInfo.Presenter;
            }

            Debug.Log($"Couldn't find the layer for presenter ({typeof(TPresenter)})");
            return null;
        }
        
        public bool IsOpened<TPresenter>() 
            where TPresenter : class, IPresenter<IView, IModel> => 
            _windowsOpenData
                .Any(windowOpenPair => windowOpenPair.Key.GetType() == typeof(TPresenter) && windowOpenPair.Key.IsOpen);

        public void Close(IPresenter<IView, IModel> windowPresenter, bool executeImmediately = false)
        {
            if (windowPresenter == null)
            {
                Debug.Log($"Can't Close ({windowPresenter.GetType()}), because it is not open");
                return;
            }
            
            if (windowPresenter.State == ViewState.Close)
            {
                return;
            }

            if (_windowsOpenData.TryGetValue(windowPresenter, out IWindowOpenInfo windowOpenInfo) == false) 
                return;
            
            Debug.Log($"Try Close ({windowPresenter.GetType()}) with Id {windowOpenInfo.Id}");
            
            _windowsOpenData.Remove(windowPresenter);
            WindowCloseEvent?.Invoke(windowOpenInfo.Presenter);
            _lastActiveWindows[windowOpenInfo.ViewLayer] = null;

            OnWindowFocusEvent?.Invoke(GetPriorityWindow(LayerName.Default).Presenter);

            if(executeImmediately)
                windowOpenInfo.Presenter.SilentClose();
            else
                windowOpenInfo.Presenter.Close();
        }

        public void ClearLayer(ViewLayer viewLayer)
        {
            foreach (var openWindow in _windowsOpenData)
            {
                if (openWindow.Value.ViewLayer != viewLayer) continue;
                
                openWindow.Key.Close();
                _windowsOpenData.Remove(openWindow.Key);
            }
            ClearedLayerEvent?.Invoke(viewLayer);
        }
        
        public void ClearLayer(string layerName)
        {
            ViewLayer viewLayer = null;
            List<IPresenter<IView, IModel>> removeList = new List<IPresenter<IView, IModel>>();
            foreach (var openWindow in _windowsOpenData)
            {
                if (openWindow.Value.ViewLayer.Name != layerName) continue;

                viewLayer = openWindow.Value.ViewLayer;
                removeList.Add(openWindow.Key);
                openWindow.Key.SilentClose();
            }

            if (viewLayer == null)
                return;
            foreach (var removeWindow in removeList)
            {
                _windowsOpenData.Remove(removeWindow);
            }
            
            removeList.Clear();
            ClearedLayerEvent?.Invoke(viewLayer);
        }
        
        public IWindowOpenInfo GetActiveWindow(ViewLayer layer)
        {
            _lastActiveWindows.TryGetValue(layer, out IWindowOpenInfo windowOpenInfo);
            return windowOpenInfo;
        }
        
        public IWindowOpenInfo GetActiveWindow(string layerName)
        {
            foreach (var window in _lastActiveWindows)
            {
                if (layerName == window.Key.Name)
                {
                    return window.Value;
                }
            }

            return null;
        }

        public IWindowOpenInfo GetPriorityWindow()
        {
            IWindowOpenInfo currentWindow = null;
            
            var layer = _viewLayerService.Layers[1];
            var layerIndex = _viewLayerService.Layers.FindIndex(viewLayer => viewLayer == layer);

            for (var i = layerIndex; i < _indexOfLastEscapableLayer; i++)
            {
                var viewLayer = _viewLayerService.Layers[i];
                var window = GetActiveWindow(viewLayer);
                
                if(window != null)
                    currentWindow = window;
            }

            return currentWindow;
        }
        
        public IWindowOpenInfo GetPriorityWindow(string layerName)
        {
            IWindowOpenInfo currentWindow = null;
            var tempLayerName = string.IsNullOrEmpty(layerName) ? LayerName.Screen : layerName;
            var layerIndex = LayerName.Layers.ToList().FindIndex(layer => layer == tempLayerName);

            for (var i = layerIndex; i < _indexOfLastEscapableLayer; i++)
            {
                var layer = LayerName.Layers[i];
                var window = GetActiveWindow(layer);
                
                if(window != null)
                    currentWindow = window;
            }

            return currentWindow;
        }

        public void Register<TPresenter>(Action<Transform, Action<IView, IModel>> createMethod)
            where TPresenter : class, IPresenter<IView, IModel>
        {
            if (IsOpened<TPresenter>())
                return;
            
            var type = typeof(TPresenter);
            var layerAttribute =
                (ViewLayerAttribute) Attribute.GetCustomAttribute(type, typeof(ViewLayerAttribute));
            Debug.Log($"Register ({type}) on layer ({layerAttribute?.Name})");

            if (layerAttribute != null)
            {
                ViewLayer layer = _viewLayerService.Layers.FirstOrDefault(x => x.Name == layerAttribute.Name);
                _presentersLayers[type] = layer;
            }

            _viewModelCreators[type] = createMethod;
        }
        
        private IWindowOpenInfo CreateWindow<TPresenter>(ViewState state = ViewState.None)
            where TPresenter : class, IPresenter<IView, IModel>
        {
            _presentersLayers.TryGetValue(typeof(TPresenter), out ViewLayer layer);
            
            var (view, model) = CreateViewModel<TPresenter>(state, _viewLayerService.GetLayerParent(layer));
            var window = _windowManagerFactory.CreateWindowPresenter<TPresenter>(view, model);
            var type = typeof(TPresenter);

            var layerAttribute =
                (ViewLayerAttribute)Attribute.GetCustomAttribute(type, typeof(ViewLayerAttribute));

            if (layerAttribute == null)
            {
                Debug.Log($"Can't register ({type}))");
                return null;
            }
            
            model.ViewLayer = layer;
            var openInfo = new WindowOpenInfo(layer)
            {
                Presenter = window
            };
            Debug.Log($"Created presenter {typeof(TPresenter)} on layer ({layerAttribute.Name})");

            _windowsOpenData[window] = openInfo;
            
            // window.Initialize();
            return openInfo;
        }
        
        private (IView, IModel) CreateViewModel<TPresenter>(ViewState state, Transform parent)
            where TPresenter : class, IPresenter<IView, IModel>
        {
            IView windowView = null;
            IModel windowModel = null;
            _viewModelCreators[typeof(TPresenter)].Invoke(parent, (view, model) =>
            {
                if (view == null)
                {
                    Debug.LogError($"Couldn't create View: {typeof(TPresenter)}");
                    return;
                }

                if (model == null)
                {
                    Debug.LogError($"Couldn't create Model: {typeof(TPresenter)}");
                }

                windowView = view;
                windowModel = model;
                windowModel.State = state;
            });

            return (windowView, windowModel);
        }
    }
}