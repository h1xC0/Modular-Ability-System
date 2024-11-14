using System;
using Core.Services.ResourceProvider;

namespace UI.MVP
{
    public interface IView : IResource, IDisposable
    {
        void Initialize();
        void Disable();
        void Enable();
    }
}