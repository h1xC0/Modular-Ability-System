using System.Collections.Generic;
using System.Linq;
using Core.Services.ResourceProvider;
using UnityEngine;

namespace Core.Services.ViewLayerService
{
    public class ViewLayerService : MonoBehaviour, IViewLayerService, IResource
    {
        [SerializeField] private List<MonoViewLayer> _layersSettings;
        public bool HasValues => _layersSettings.Count > 0;

        private List<ViewLayer> _layers;
        public List<ViewLayer> Layers => _layers;
        
        private int _currentLayerOrder;
  
        public void SetupWindowLayers()
        {
            _layers = new List<ViewLayer>();
            
            foreach (var layerSettings in _layersSettings)
            {
                var layer = new ViewLayer(layerSettings)
                {
                    Order = _currentLayerOrder++
                };
                
                _layers.Add(layer);
            }

            _currentLayerOrder = 0;
        }

        public void AddLayer(MonoViewLayer monoViewLayer)
        {
            _layersSettings.Add(monoViewLayer);
        }

        public void ClearLayers()
        {
            _layersSettings.Clear();
        }

        public Transform GetLayerParent(ViewLayer viewLayer) => 
            _layersSettings.FirstOrDefault(layerSettings => layerSettings.LayerType == viewLayer.Name)?.transform;
        
    }
}