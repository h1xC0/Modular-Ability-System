namespace Core.Services.ViewLayerService
{
    [System.Serializable]
    public class ViewLayer
    {
        private readonly MonoViewLayer _settings;

        public string Name => Settings.LayerType;
        
        public int Order { get; set; }

        public MonoViewLayer Settings => _settings;
        
        public ViewLayer(MonoViewLayer layer)
        {
            _settings = layer;
        }

        public override string ToString()
        {
            return $"Layer: {Name}, Order: {Order}";
        }
    }
}