using System;
using Core.Services.ViewLayerService;

namespace UI.MVP
{
    public interface IModel : IDisposable
    {
        ViewState State { get; set; }
        ViewLayer ViewLayer { get; set; }
    }
}