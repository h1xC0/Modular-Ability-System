using System;
using Core.Services.WindowAnimation;
using DG.Tweening;
using UI.UIExtensions;
using UI.WindowSystem;
using Zenject;

namespace UI.MVP.Advanced
{
    public class WindowPresenter<TWindowView, TWindowModel> : Presenter<TWindowView, TWindowModel>
    where TWindowView : IWindowView
    where TWindowModel : IModel
    {
        protected readonly IWindowAnimationService WindowAnimationService;

        [Inject] private IWindowManager _windowManger;

        protected WindowPresenter(
            TWindowView viewContract,
            TWindowModel modelContract,
            IWindowAnimationService windowAnimationService) 
            : base(viewContract, modelContract)
        {
            WindowAnimationService = windowAnimationService;

            OpenEvent += OpenView;
            CloseEvent += CloseView;
            View.OpenEvent += OnOpen;
            View.CloseEvent += OnClose;
            View.OpenCompleteEvent += OnCompleteEvent;
            
        }

        private void OpenView()
        {
            View.ViewAnimation.Initialize(WindowAnimationService);
            
            switch (State)
            {
                case ViewState.None:
                    View.Open(false);
                    break;
                case ViewState.Open:
                    View.Enable();
                    View.Open(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CloseView()
        {
            switch (State)
            {
                case ViewState.Close:
                    View.Close(true);
                    break;
                case ViewState.SilentClose:
                    View.Close(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnOpen(bool animated)
        {
            if (animated == false) return;

            View.ViewAnimation.ViewSequence(ViewAnimation.In)
            .OnComplete(View.OnOpenComplete)
            .Play();
        }

        private void OnCompleteEvent()
        {
            View.CloseAction += CloseThis;
        }

        private void OnClose(bool animated)
        {
            if (animated == false)
            {
                View.OnCloseComplete();
                OnCloseWindow();
                return;
            }
            
            _windowManger.Close((IPresenter<IView, IModel>)this);
            View.ViewAnimation.ViewSequence(ViewAnimation.Out)
                .OnComplete(() =>
                {
                    View.OnCloseComplete();
                    OnCloseWindow();
                })
                .Play();
        }

        protected override void OnDispose()
        {
            OpenEvent -= OpenView;
            CloseEvent -= CloseView;
            View.OpenEvent -= OnOpen;
            View.CloseEvent -= OnClose;
            
            View.CloseAction -= CloseThis;
            base.OnDispose();
        }

        private void CloseThis()
        {
            _windowManger.Close((IPresenter<IView, IModel>)this);
        }
    }
}