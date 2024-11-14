using Core.Services.ViewLayerService;
using UI.MVP;

namespace UI.WindowSystem
{
    public sealed class WindowOpenInfo : IWindowOpenInfo
    {
        public int Id { get; }

        public IPresenter<IView, IModel> Presenter { get; set; }
        public ViewLayer ViewLayer { get; }

        public WindowOpenInfo(ViewLayer viewLayer)
        {
            ViewLayer = viewLayer;
            Id = GetHashCode();
        }
    }
}