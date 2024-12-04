using Game.Combat.Abilities;
using Shared.Extensions.Rx;
using UI.MVP;

namespace UI.Windows.HUDWindow
{
    public class HudModel : Model, IHudModel
    {
        public IReactiveCollection<AbilityConfiguration> AbilityConfigurations { get; }

        public HudModel()
        {
            AbilityConfigurations = new ReactiveCollection<AbilityConfiguration>();
        }
    }
}
