using System;
using Core.Services.ViewLayerService;

namespace UI.MVP
{
    public interface IPresenter<out TViewContract, out TModelContract> 
        where TViewContract : IView
        where TModelContract : IModel
    {
        TViewContract View { get; }
        TModelContract Model { get; }

        event Action OpenEvent;
        event Action CloseEvent;
        
        void Open();
        void Close();

        void SilentClose();
        ViewLayer Layer { get; }

        ViewState State { get; }

        bool IsOpen { get; }
    }
}