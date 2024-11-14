using System;
using System.Collections.Generic;
using Core.Services.ViewLayerService;

namespace UI.MVP
{
    public class Presenter<TViewContract, TModelContract> : IPresenter<TViewContract, TModelContract>, IDisposable
        where TViewContract : IView
        where TModelContract : IModel
    {
        public TViewContract View { get; protected set; }
        public TModelContract Model { get; protected set; }

        protected List<IDisposable> CompositeDisposable
        {
            get
            {
                if (_compositeDisposable != null)
                    return _compositeDisposable;
                
                _compositeDisposable = new List<IDisposable>();
                return _compositeDisposable;
            }
        }

        public ViewLayer Layer => Model.ViewLayer;
        public ViewState State => Model.State;
        public bool IsOpen => Model.State == ViewState.Open;

        public event Action OpenEvent;
        public event Action CloseEvent;

        private List<IDisposable> _compositeDisposable;

        protected Presenter(TViewContract viewContract, TModelContract modelContract)
        {
            View = viewContract;
            Model = modelContract;
        }
        
        public void OnCloseWindow()
        {
            CompositeDisposable.Add(Model);
            CompositeDisposable.Add(View);

            OnDispose(); 
        }
        
        protected virtual void OnDispose()
        {
            Dispose();
        }

        public void Dispose()
        {
            foreach (var disposable in _compositeDisposable)
            {
                disposable.Dispose();
            }
        }

        public virtual void Open()
        {
            UpdateViewState();
            OpenEvent?.Invoke();
        }

        public virtual void Close()
        {
            CloseWindow();
        }
        
        public virtual void SilentClose()
        {
            CloseWindow(true);
        }

        private void UpdateViewState()
        {
            View.Initialize();

            switch (State)
            {
                case ViewState.None:
                    View.Disable();
                    break;
                case ViewState.Open:
                    View.Enable();
                    break;
            }
        }
        
        private void CloseWindow(bool silentClose = false)
        {
            Model.State = silentClose == false ? ViewState.Close : ViewState.SilentClose;
            CloseEvent?.Invoke();
        }
    }
}
