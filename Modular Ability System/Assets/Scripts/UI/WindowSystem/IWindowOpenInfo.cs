using Core.Services.ViewLayerService;
using UI.MVP;

namespace UI.WindowSystem
{
    public interface IWindowOpenInfo
    {
        int Id { get; }
        IPresenter<IView, IModel> Presenter { get; }
        ViewLayer ViewLayer { get; }
    }
}