using Core.Services.ViewLayerService;

namespace UI.MVP
{
    public class Model : IModel
    {
        public ViewState State { get; set; } = new();
        public ViewLayer ViewLayer { get; set; }

        public virtual void Dispose()
        {
            
        }
    }
}