using System.Linq;
using Core.Services.ResourceProvider;
using Core.Services.ViewLayerService;
using Core.Services.WindowAnimation;
using Game.Combat.Abilities;
using UI.MVP.Advanced;

namespace UI.Windows.HUDWindow
{
    [ViewLayer(LayerName.Default)]
    public class HudPresenter : WindowPresenter<IHudView, IHudModel>
    {
        private readonly IResourceProviderService _resourceProviderService;

        protected HudPresenter(HudView viewContract,
            HudModel modelContract,
            IWindowAnimationService windowAnimationService,
            IResourceProviderService resourceProviderService) 
            : base(viewContract,
            modelContract,
            windowAnimationService)
        {
            _resourceProviderService = resourceProviderService;
            LoadAbilities();
            View.Initialize(Model.AbilityConfigurations.Collection.ToList());
        }

        private void LoadAbilities()
        {
            var configurations = _resourceProviderService.LoadResources<AbilityConfiguration>();

            foreach (var configuration in configurations)
            {
                Model.AbilityConfigurations.Add(configuration);
            }
        }
    }
}
