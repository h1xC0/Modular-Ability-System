using System;

namespace UI.MVP
{
    public interface IManagedView : IView
    {
        event Action<bool> OpenEvent;
        event Action<bool> CloseEvent;
        event Action CloseCompleteEvent;
        event Action OpenCompleteEvent;

        event Action CloseAction;

        void Open(bool animated);
        void Close(bool animated);
        void OnOpenComplete();
        void OnCloseComplete();
    }
}