using System.Collections.Generic;
using UnityEngine;

namespace Core.Services.ViewLayerService
{
    public interface IViewLayerService
    {
        void AddLayer(MonoViewLayer monoViewLayer);
        void ClearLayers();
        bool HasValues { get; }
        List<ViewLayer> Layers { get; }
        void SetupWindowLayers();
        Transform GetLayerParent(ViewLayer viewLayer);
    }
}