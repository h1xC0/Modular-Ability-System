using Core.Services.ViewLayerService;
using Core.Services.WindowAnimation;
using UI.MVP.Advanced;

namespace UI.Windows.HUDWindow
{
    [ViewLayer(LayerName.Popup)]
    public class HudPresenter : WindowPresenter<HudView, HudModel>
    {
        protected HudPresenter(HudView viewContract, HudModel modelContract, IWindowAnimationService windowAnimationService) : base(viewContract, modelContract, windowAnimationService)
        {
            
        }
    }
}
